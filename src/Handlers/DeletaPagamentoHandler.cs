using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class DeletaPagamentoHandler
    {
        public static async Task<IResult> DeletaPagamento(Guid id, Guid idPagamento, IPagamentoService pagamentoService)
        {
            await pagamentoService.DeletaPagamento(id.ToString(), idPagamento.ToString());

            return Results.Ok();
        }
    }
}
