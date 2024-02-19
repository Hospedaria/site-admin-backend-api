using FluentValidation;
using Hospedaria.Reservas.Api.Entities;

namespace Hospedaria.Reservas.Api.Validations
{
    public class PagamentoValidator : AbstractValidator<Pagamento>
    {
        public PagamentoValidator()
        {
            RuleFor(c => c.IdReserva)
                .NotEmpty().WithMessage("Id da reserva é obrigatório")
                .NotNull().WithMessage("Id da reserva é obrigatório");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .NotNull().WithMessage("Descrição é obrigatória");

            RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage("Valor do sinal deve ser maior do que 0");
        }
    }
}
