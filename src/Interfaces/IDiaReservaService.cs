using Hospedaria.Reservas.Api.Entities;

namespace Hospedaria.Reservas.Api.Interfaces
{
    public interface IDiaReservaService
    {
        Task CadastrarDias(Reserva reserva);
        Task<List<DiaReserva>> ConsultaReservasAPartirDeHoje();
        Task<List<DiaReserva>> ConsultaReservasPorPeriodo(DateTime dataInicio, DateTime dataFinal);
        Task Deletar(Reserva reserva);
        Task Atualizar(Reserva reserva);
    }
}
