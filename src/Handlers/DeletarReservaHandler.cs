﻿using Hospedaria.Reservas.Api.Interfaces;

namespace Hospedaria.Reservas.Api.Handlers
{
    public static class DeletarReservaHandler
    {
        public static async Task<IResult> DeletarReserva(Guid id,
            IReservaService reservaService,
            IDiaReservaService diaReservaService)
        {
            if (Guid.Empty == id)
                return Results.BadRequest("Id inválido");

            var reserva = await reservaService.ConsultarReserva(id.ToString());

            await reservaService.DeletarReserva(id.ToString());
            await diaReservaService.Deletar(reserva);

            return Results.NoContent();
        }
    }
}
