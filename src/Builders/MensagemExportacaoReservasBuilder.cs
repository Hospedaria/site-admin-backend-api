using Hospedaria.Reservas.Api.Entities;
using System.Globalization;
using System.Text;

namespace Hospedaria.Reservas.Api.Builders
{
    public class MensagemExportacaoReservasBuilder
    {
        public MensagemExportacaoReservasBuilder()
        {
            Mensagem = new();
        }

        public StringBuilder Mensagem { get; }

        public MensagemExportacaoReservasBuilder Build(List<Reserva> reservas, DateTime dataReferencia)
        {
            Mensagem.AppendLine($"{dataReferencia.ToString("ddd, d MMM", new CultureInfo("pt-BR"))}");
            Mensagem.AppendLine();

            foreach (var reserva in reservas)
            {
                Mensagem.AppendLine($"*{reserva.Nome}*");

                Mensagem.AppendLine("Suites:");
                foreach (var suite in reserva.Suites)
                    Mensagem.AppendLine($"- {SuitesDisponiveis.DeParaMensagemExportacao.GetValueOrDefault(suite)}");

                if (reserva.Pagamentos.Any())
                {
                    Mensagem.AppendLine("Depósitos:");
                    foreach (var pagamento in reserva.Pagamentos)
                        Mensagem.AppendLine($"R$ {pagamento.Valor.ToString("0.##")} - {pagamento.DataPagamento:dd/MM/yyyy}");
                }

                Mensagem.AppendLine($"Valor a pagar: R$ {reserva.ValorAPagar.ToString("0.##")}");
                Mensagem.AppendLine();
            }

            return this;
        }
    }
}
