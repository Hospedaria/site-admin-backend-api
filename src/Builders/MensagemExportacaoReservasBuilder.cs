using Hospedaria.Reservas.Api.Entities;
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
            Mensagem.AppendLine($"{dataReferencia:ddd, d MMM}");
            Mensagem.AppendLine();

            foreach (var reserva in reservas)
            {
                Mensagem.AppendLine($"{reserva.Nome}");

                Mensagem.AppendLine("Suites:");
                foreach (var suite in reserva.Suites)
                    Mensagem.AppendLine($"- {SuitesDisponiveis.DeParaMensagemExportacao.GetValueOrDefault(suite)}");

                if (reserva.Pagamentos.Any())
                {
                    Mensagem.AppendLine("Depósitos:");
                    foreach (var pagamento in reserva.Pagamentos)
                        Mensagem.AppendLine($"R$ {pagamento.Valor} - {pagamento.DataPagamento:dd/MM/yyyy}");
                }

                Mensagem.AppendLine($"Valor a pagar:{reserva.ValorAPagar}");
                Mensagem.AppendLine();
            }

            return this;
        }
    }
}
