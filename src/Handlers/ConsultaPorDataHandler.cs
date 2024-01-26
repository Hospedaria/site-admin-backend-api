using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ConsultaPorDataHandler
    {
        public static async Task<IResult> Consultar(DateTime data, IReservaService reservaService)
        {
            var reservas = await reservaService.ConsultarReservas(data);

            if (reservas.Any())
                return Results.Ok(reservas);

            return Results.NoContent();
        }
    }
}
