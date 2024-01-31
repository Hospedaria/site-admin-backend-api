using Hospedaria.Reservas.Api.Entities;

namespace Hospedaria.Reservas.Api.Interfaces
{
    public interface IReservaService
    {
        Task SalvarReserva(Reserva reserva);
        Task<Reserva> ConsultarReserva(string id);
        Task<List<Reserva>> ConsultarReservas(DateTime dataReserva);
        Task DeletarReserva(string id);
        Task<List<Reserva>> ConsultarReservasAPartirDeHoje();
    }
}
