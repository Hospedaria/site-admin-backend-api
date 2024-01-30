using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class DeletarReservaHandler
    {
        public static async Task<IResult> DeletarReserva(Guid id, IReservaService reservaService)
        {
            if (Guid.Empty == id)
                return Results.BadRequest("Id inválido");

            await reservaService.DeletarReserva(id.ToString());

            return Results.NoContent();
        }
    }
}
