using FluentValidation;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class CadastraPagamentoHandler
    {
        public static async Task<IResult> CadastrarPagamento(Pagamento pagamento,
            IValidator<Pagamento> validator,
            IPagamentoService pagamentoService)
        {
            var resultado = validator.Validate(pagamento);
            if (!resultado.IsValid) return Results.ValidationProblem(resultado.ToDictionary());

            await pagamentoService.InserePagamento(pagamento);

            return Results.Ok();
        }
    }
}
