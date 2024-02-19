using Amazon.DynamoDBv2.DataModel;
using Hospedaria.Reservas.Api.Entities;

namespace Hospedaria.Reservas.Api.Interfaces
{
    public interface IPagamentoService
    {
        Task<List<Pagamento>> BuscaPagamentosDaReserva(string idReserva);

        Task InserePagamento(Pagamento pagamento);

        Task DeletaPagamento(string idReserva, string idPagamento);

        Task DeletaPagamentosDaReserva(string idReserva);
    }
}
