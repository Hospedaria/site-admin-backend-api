using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class BuscaReservaHandler
    {
        public static async Task<IResult> BuscarReserva(Guid id, IReservaService reservaService,
            IPagamentoService pagamentoService)
        {
            if (Guid.Empty == id)
                return Results.BadRequest("Id inválido");

            var reserva = await reservaService.ConsultarReserva(id.ToString());
            if (reserva == null)
                return Results.NoContent();

            reserva.Pagamentos = await pagamentoService.BuscaPagamentosDaReserva(reserva.Id);

            return Results.Ok(reserva);
        }
    }
}
