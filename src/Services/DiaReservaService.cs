using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;
using Microsoft.VisualBasic;
using System.Security.Cryptography.Xml;

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

            return await Context.ScanAsync<DiaReserva>(new List<ScanCondition>()
            {
                new(nameof(DiaReserva.Data), ScanOperator.GreaterThanOrEqual, dataReferencia)
            }, new DynamoDBOperationConfig()
            {
                OverrideTableName = DiaReserva.GetNomeTabela()
            }).GetRemainingAsync();
        }

        public async Task<List<DiaReserva>> ConsultaReservasPorPeriodo(DateTime dataInicio, DateTime dataFinal)
        {
            return await Context.ScanAsync<DiaReserva>(new List<ScanCondition>()
            {
                new(nameof(DiaReserva.Data), ScanOperator.Between, dataInicio, dataFinal)
            }, new DynamoDBOperationConfig()
            {
                OverrideTableName = DiaReserva.GetNomeTabela()
            }).GetRemainingAsync();
        }

        public async Task Deletar(Reserva reserva)
        {
            var diasReserva = await Context.QueryAsync<DiaReserva>(reserva.Id, new DynamoDBOperationConfig()
            {
                OverrideTableName = DiaReserva.GetNomeTabela(),
                IndexName = "ix_reserva"
            }).GetRemainingAsync();

            diasReserva.ForEach(async (dia) => await Context.DeleteAsync(dia, new DynamoDBOperationConfig()
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
