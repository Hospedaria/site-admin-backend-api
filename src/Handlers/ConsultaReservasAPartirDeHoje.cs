using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ConsultaReservasAPartirDeHoje
    {
        public static async Task<IResult> Consultar(IReservaService reservaService)
        {
            var reservas = await reservaService.ConsultarReservasAPartirDeHoje();
            if (reservas.Any())
                return Results.Ok(reservas);
            else
                return Results.NoContent();
        }
    }
}
