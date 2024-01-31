using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ConsultaPorPeriodo
    {
        public static async Task<IResult> Consultar(DateTime dataInicio,
            DateTime dataTermino,
            IReservaService reservaService)
        {
            if (dataTermino < dataInicio)
                return Results.ValidationProblem(new Dictionary<string, string[]>()
                {
                    {"DataTermino", new[]{ "Data de término deve ser maior do que a data de início" } }
                });
            
            var reservas = await reservaService.ConsultarReservasPorPeriodo(dataInicio, dataTermino);

            if (reservas.Any())
                return Results.Ok(reservas.OrderBy(c => c.CheckIn));

            return Results.NoContent();
        }
    }
}
