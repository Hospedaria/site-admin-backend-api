using Hospedaria.Reservas.Api.Extensions;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ConsultaReservasAPartirDeHoje
    {
        public static async Task<IResult> Consultar(IReservaService reservaService, 
            IDiaReservaService diaReservaService)
        {
            var dias = await diaReservaService.ConsultaReservasAPartirDeHoje();

            var reservas = await reservaService.ConsultarEmLote(dias.BuscarReservas());
            if (reservas.Any())
                return Results.Ok(reservas.OrderBy(c => c.CheckIn));
            else
                return Results.NoContent();
        }
    }
}
