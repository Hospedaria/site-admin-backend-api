using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
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

        public async Task DeletarReserva(string id)
        {
            await DbContext.DeleteAsync<Reserva>(id, new DynamoDBOperationConfig()
            {
                OverrideTableName = Reserva.GetNomeTabela()
            });
        }

        public async Task<List<Reserva>> ConsultarEmLote(List<string> ids)
        {
            var batch = DbContext.CreateBatchGet<Reserva>(new()
            {
                OverrideTableName = Reserva.GetNomeTabela()
            });

            ids.ForEach((id)=> batch.AddKey(id));
            await batch.ExecuteAsync();

            return batch.Results;
        }
    }
}
