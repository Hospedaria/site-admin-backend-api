using Hospedaria.Reservas.Api.Entities;

namespace Hospedaria.Reservas.Api.Builders
{
    public class ReservaGridSiteBuilder
    {
        public ReservaGridSiteBuilder()
        {
            Items = new();
        }

        public List<ReservaGridSite> Items { get; private set; }

        public ReservaGridSiteBuilder BuildList(List<DiaReserva> dias, List<Reserva> reservas)
        {
            dias.ForEach((dia) =>
            {
                var idsReservas = dias.Where(c => c.Data == dia.Data)
                    .Select(c => c.IdReserva);

                List<Reserva> reservasParaInserir = new();
                foreach (var item in idsReservas)
                    reservasParaInserir.Add(reservas.First(c => c.Id == item));
                
                Items.Add(new(dia.Data, reservasParaInserir));
            });

            return this;
        }
    }
}
