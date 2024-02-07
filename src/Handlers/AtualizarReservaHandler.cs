using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class AtualizarReservaHandler
    {
        public static async Task<IResult> AtualizarReserva(Guid id,
            Reserva reserva,
            IReservaService reservaService,
            IDiaReservaService diaReservaService)
        {
            if (Guid.Empty == id)
                return Results.BadRequest("Id inválido");

            var reservaDB = await reservaService.ConsultarReserva(id.ToString());

            if (reservaDB == null)
                return Results.BadRequest("Reserva não encontrada");

            await reservaService.SalvarReserva(reserva);
            await diaReservaService.Atualizar(reserva);

            return Results.Ok();
        }
    }
}
