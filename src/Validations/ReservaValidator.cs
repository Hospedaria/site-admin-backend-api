using FluentValidation;
using Hospedaria.Reservas.Api.Entities;
using System.Globalization;
using System.Net.Mail;

namespace Hospedaria.Reservas.Api.Validations
{
    public class ReservaValidator : AbstractValidator<Reserva>
    {
        public ReservaValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id da reserva é obrigatório")
                .NotEqual(Guid.Empty.ToString()).WithMessage("Id da reserva é obrigatório");

            RuleFor(c => c.Email)
                .NotNull().WithMessage("Email não pode ser nulo")
                .NotEmpty().WithMessage("Email não pode ser vazio")
                .MaximumLength(200).WithMessage("Email deve ter no máximo 200 caracteres");
            When(c => !string.IsNullOrEmpty(c.Email), () => {
                RuleFor(c => c.Email)
                    .Custom((e, ct) =>
                    {
                        if (e.Length < 200 && !MailAddress.TryCreate(e, out _))
                            ct.AddFailure("Email inválido");
                    });
            });

            RuleFor(c => c.Telefone)
                .NotNull().WithMessage("Telefone não pode ser nulo")
                .NotEmpty().WithMessage("Telefone não pode ser vazio");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Nome não pode ser vazio")
                .NotNull().WithMessage("Nome não pode ser nulo")
                .MaximumLength(200).WithMessage("Nome não pode ter mais do que 200 caracteres")
                .MinimumLength(2).WithMessage("Nome deve ter ao menos 2 caracteres");

            RuleFor(c => c.CheckIn)
                .NotNull().WithMessage("Check-in não pode ser nulo")
                .LessThan(c => c.CheckOut).WithMessage("Check-out deve ser maior do que o Check-in");

            RuleFor(c => c.CheckOut)
                .NotNull().WithMessage("Check-out não pode ser nulo");

            RuleFor(c => c.ChegaraAs)
                .NotNull().WithMessage("Chegará às não pode ser nulo")
                .Custom((c, ct) =>
                {
                    if (!string.IsNullOrEmpty(c) && 
                        TimeSpan.TryParseExact(c,
                        "HH:mm",
                        CultureInfo.InvariantCulture,
                        out _))
                        ct.AddFailure("Horário de chegada inválido");
                });

            RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage("Valor da reserva deve ser maior do que 0");

            RuleFor(c => c.QuantidadeAdultos)
                .GreaterThanOrEqualTo((short)1).WithMessage("Quantidade de adultos deve ser de no mínimo 1");

            RuleFor(c => c.QuantidadeCriancas)
                .GreaterThanOrEqualTo((short)0).WithMessage("Quantidade de crianças deve ser no mínimo 0");

            RuleFor(c => c.Observacoes)
                .MaximumLength(255).WithMessage("Observações devem ter no máximo 255 caracteres");

            RuleFor(c => c.Suites)
                .NotEmpty().WithMessage("Suites são obrigatórias")
                .Custom((s, ct) =>
                {
                    s.ForEach((c) => {
                        if (!SuitesDisponiveis.Suites.Exists(s => s.Id == c))
                            ct.AddFailure("Suite inválida selecionada, ela não existe");
                    });
                });

        }
    }
}
