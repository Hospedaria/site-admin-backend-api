using Hospedaria.Reservas.Api.Entities;

namespace Hospedaria.Reservas.Api.Interfaces
{
    public interface IReservaService
    {
        Task SalvarReserva(Reserva reserva);
        Task<Reserva> ConsultarReserva(string id);
        Task<List<Reserva>> ConsultarReservasPorPeriodo(DateTime dataInicio, DateTime dataTermino);
        Task DeletarReserva(string id);
        Task<List<Reserva>> ConsultarReservasAPartirDeHoje();
    }
}
