using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class BuscaReservaHandler
    {
        public static async Task<IResult> BuscarReserva(Guid id, IReservaService reservaService)
        {
            if (Guid.Empty == id)
                return Results.BadRequest("Id inválido");

            var reserva = await reservaService.ConsultarReserva(id);
            if (reserva == null)
                return Results.NoContent();

            return Results.Ok(reserva);
        }
    }
}
