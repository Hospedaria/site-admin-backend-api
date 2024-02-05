using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;
using Microsoft.VisualBasic;

namespace Hospedaria.Reservas.Api.Services
{
    public class DiaReservaService : IDiaReservaService
    {
        public DiaReservaService(IAmazonDynamoDB dynamoDB)
        {
            Context = new DynamoDBContext(dynamoDB);
        }

        IDynamoDBContext Context { get; }


        public async Task CadastrarDias(Reserva reserva)
        {
            DateTime dataInicial = Convert.ToDateTime(reserva.CheckIn);
            DateTime dataFinal = Convert.ToDateTime(reserva.CheckOut);
            DateTime dataReferencia = dataInicial;
            List<DiaReserva> diasReserva = new();

            while (dataReferencia <= dataFinal)
            {
                diasReserva.Add(new(reserva.Id, dataReferencia));
                dataReferencia = dataReferencia.AddDays(1);
            }

            var batchWrite = Context.CreateBatchWrite<DiaReserva>(new()
            {
                OverrideTableName = DiaReserva.GetNomeTabela()
            });

            batchWrite.AddPutItems(diasReserva);
            
            await batchWrite.ExecuteAsync();
        }

        public async Task<List<DiaReserva>> ConsultaReservasAPartirDeHoje()
        {
            DateTime dataReferencia = DateTime.UtcNow.AddHours(-3);

            return await Context.QueryAsync<DiaReserva>("data",
                QueryOperator.GreaterThanOrEqual,
                new List<object>() { dataReferencia},
                new()
                {
                    OverrideTableName = DiaReserva.GetNomeTabela()
                })
            .GetRemainingAsync();
        }

        public async Task<List<DiaReserva>> ConsultaReservasPorPeriodo(DateTime dataInicio, DateTime dataFinal)
        {
            return await Context.QueryAsync<DiaReserva>("data",
                QueryOperator.Between,
                new List<object>() { dataInicio, dataFinal },
                new()
                {
                    OverrideTableName = DiaReserva.GetNomeTabela()
                }).GetRemainingAsync();
        }

        public async Task Deletar(Reserva reserva)
        {
            DateTime dataInicio = Convert.ToDateTime(reserva.CheckIn);
            DateTime dataTermino = Convert.ToDateTime(reserva.CheckOut);


            var dias = await ConsultaReservasPorPeriodo(dataInicio, dataTermino);

            dias.ForEach(async (dia) => await Context.DeleteAsync(dia, new DynamoDBOperationConfig()
            {
                OverrideTableName = DiaReserva.GetNomeTabela()
            }));
        }

        public async Task Atualizar(Reserva reserva)
        {
            await Deletar(reserva);
            await CadastrarDias(reserva);
        }
    }
}
