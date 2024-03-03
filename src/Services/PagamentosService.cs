using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Services
{
    public class PagamentosService : IPagamentoService
    {

        public PagamentosService(IAmazonDynamoDB dynamoDBClient)
        {
            DbContext = new DynamoDBContext(dynamoDBClient);
        }

        IDynamoDBContext DbContext { get; }

        public async Task<List<Pagamento>> BuscaPagamentosDaReserva(string idReserva)
        {
            return await DbContext.QueryAsync<Pagamento>(idReserva, new DynamoDBOperationConfig()
            {
                OverrideTableName = Pagamento.GetNomeTabela()
            }).GetRemainingAsync();
        }

        public async Task InserePagamento(Pagamento pagamento)
        {
            await DbContext.SaveAsync(pagamento, new DynamoDBOperationConfig()
            {
                OverrideTableName = Pagamento.GetNomeTabela()
            });
        }

        public async Task DeletaPagamento(string idReserva, string idPagamento)
        {
            await DbContext.DeleteAsync<Pagamento>(hashKey: idReserva, rangeKey: idPagamento, new DynamoDBOperationConfig()
            {
                OverrideTableName = Pagamento.GetNomeTabela()
            });
        }

        public async Task DeletaPagamentosDaReserva(string idReserva)
        {
            var pagamentos = await BuscaPagamentosDaReserva(idReserva);

            foreach (var pagamento in pagamentos)
                await DeletaPagamento(idReserva, pagamento.IdPagamento);
        }
    }
}
