using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Services
{
    public class ReservaService : IReservaService
    {
        public ReservaService(IAmazonDynamoDB dynamoDB)
        {
            DbContext = new DynamoDBContext(dynamoDB);
        }

        IDynamoDBContext DbContext { get; }

        public async Task SalvarReserva(Reserva reserva)
        {
            await DbContext.SaveAsync(reserva, new DynamoDBOperationConfig(){
                OverrideTableName = Reserva.GetNomeTabela()
            });
        }

        public async Task<Reserva> ConsultarReserva(string id)
        {
            return await DbContext.LoadAsync<Reserva>(id, new DynamoDBOperationConfig()
            {
                OverrideTableName = Reserva.GetNomeTabela()
            });
        }

        public async Task<List<Reserva>> ConsultarReservasAPartirDeHoje()
        {
            DateTime dataReferencia = DateTime.UtcNow.AddHours(-3);

            return await DbContext.ScanAsync<Reserva>(new List<ScanCondition>()
            {
                new("CheckIn", ScanOperator.GreaterThanOrEqual, dataReferencia)
            }, new DynamoDBOperationConfig()
            {
                OverrideTableName = Reserva.GetNomeTabela()
            }).GetRemainingAsync();
        }

        public async Task<List<Reserva>> ConsultarReservasPorPeriodo(DateTime dataInicio, DateTime dataTermino)
        {
            List<Reserva> reservas = new();

            DateTime dataRef = dataInicio;

            while (dataRef <= dataTermino)
            {
                reservas.AddRange(await DbContext.QueryAsync<Reserva>(dataRef, new DynamoDBOperationConfig()
                {
                    IndexName = "ix_checkin",
                    OverrideTableName = Reserva.GetNomeTabela()
                }).GetRemainingAsync());

                dataRef = dataRef.AddDays(1);
            }

            return reservas;
        }

        public async Task DeletarReserva(string id)
        {
            await DbContext.DeleteAsync<Reserva>(id, new DynamoDBOperationConfig()
            {
                OverrideTableName = Reserva.GetNomeTabela()
            });
        }
    }
}
