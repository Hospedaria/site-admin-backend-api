using Hospedaria.Reservas.Api.Builders;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Extensions;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class ExportarReservas
    {
        public static async Task<IResult> Exportar(DateTime dataReferencia, IDiaReservaService diaReservaService, IReservaService reservaService,
                IPagamentoService pagamentoService)
        {
            var diasReservas = await diaReservaService.ConsultaReservasPorPeriodo(dataReferencia, dataReferencia);
            List<Reserva> reservas = await reservaService.ConsultarEmLote(diasReservas.BuscarReservas());
            foreach (var reserva in reservas)
            {
                reserva.Pagamentos = await pagamentoService.BuscaPagamentosDaReserva(reserva.Id);
            }

            MensagemExportacaoReservasBuilder mensagemBuilder = new MensagemExportacaoReservasBuilder()
                .Build(reservas, dataReferencia);

            return Results.Ok(mensagemBuilder.Mensagem.ToString());
        }
    }
}
