using Hospedaria.Reservas.Api.Extensions;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ConsultaPorPeriodo
    {
        public static async Task<IResult> Consultar(DateTime dataInicio,
            DateTime dataTermino,
            IReservaService reservaService, 
            IDiaReservaService diaReservaService)
        {
            if (dataTermino < dataInicio)
                return Results.ValidationProblem(new Dictionary<string, string[]>()
                {
                    {"DataTermino", new[]{ "Data de término deve ser maior do que a data de início" } }
                });

            var diasReserva = await diaReservaService.ConsultaReservasPorPeriodo(dataInicio, dataTermino);

            var reservas = await reservaService.ConsultarEmLote(diasReserva.BuscarReservas());

            if (reservas.Any())
                return Results.Ok(reservas.OrderBy(c => c.CheckIn));

            return Results.NoContent();
        }
    }
}
