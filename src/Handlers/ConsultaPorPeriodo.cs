using Hospedaria.Reservas.Api.Builders;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Extensions;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ConsultaPorPeriodo
    {
        public static async Task<IResult> Consultar(DateTime dataInicio,
            DateTime dataTermino,
            IReservaService reservaService, 
            IDiaReservaService diaReservaService,
            IPagamentoService pagamentoService)
        {
            if (dataTermino < dataInicio)
                return Results.ValidationProblem(new Dictionary<string, string[]>()
                {
                    {"DataTermino", new[]{ "Data de término deve ser maior do que a data de início" } }
                });

            var diasReserva = await diaReservaService.ConsultaReservasPorPeriodo(dataInicio, dataTermino);

            var reservas = await reservaService.ConsultarEmLote(diasReserva.BuscarReservas());

            foreach (var reserva in reservas)
            {
                reserva.Pagamentos = await pagamentoService.BuscaPagamentosDaReserva(reserva.Id);
            }

            List<ReservaGridSite> reservasSite = new ReservaGridSiteBuilder()
                .BuildList(diasReserva, reservas)
                .Items;

            if (reservasSite.Any())
                return Results.Ok(reservasSite.OrderBy(c => c.Data));

            return Results.NoContent();
        }
    }
}
