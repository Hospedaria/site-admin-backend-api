using Hospedaria.Reservas.Api.Builders;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Extensions;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ConsultaReservasAPartirDeHoje
    {
        public static async Task<IResult> Consultar(IReservaService reservaService,
            IDiaReservaService diaReservaService,
            IPagamentoService pagamentoService)
        {
            var dias = await diaReservaService.ConsultaReservasAPartirDeHoje();
            var reservas = await reservaService.ConsultarEmLote(dias.BuscarReservas());

            foreach (var reserva in reservas)
            {
                reserva.Pagamentos = await pagamentoService.BuscaPagamentosDaReserva(reserva.Id);
            }

            List<ReservaGridSite> reservasSite = new ReservaGridSiteBuilder()
                .BuildList(dias, reservas)
                .Items;

            if (reservasSite.Any())
                return Results.Ok(reservasSite.OrderBy(c => c.Data));
            else
                return Results.NoContent();
        }
    }
}