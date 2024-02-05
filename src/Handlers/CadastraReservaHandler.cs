using FluentValidation;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class CadastraReservaHandler
    {
        public static async Task<IResult> CadastrarReserva(Reserva reserva,
            IReservaService cadastraReservaService,
            IDiaReservaService diaReservaService,
            IValidator<Reserva> validador)
        {
            var resultadoValidacao = await validador.ValidateAsync(reserva);
            if (!resultadoValidacao.IsValid)
                return Results.ValidationProblem(resultadoValidacao.ToDictionary());

            await cadastraReservaService.SalvarReserva(reserva);
            await diaReservaService.CadastrarDias(reserva);

            return Results.Ok();
        }
    }
}
