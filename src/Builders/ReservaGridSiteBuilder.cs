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
            var datas = dias.Select(c => c.Data).DistinctBy(c => c.Date).ToList();

            datas.ForEach((data) =>
            {
                var idsReservas = dias.Where(c => c.Data == data)
                    .Select(c => c.IdReserva);

                List<Reserva> reservasParaInserir = new();
                foreach (var item in idsReservas)
                    reservasParaInserir.Add(reservas.First(c => c.Id == item));
                
                Items.Add(new(data, reservasParaInserir));
            });

            return this;
        }
    }
}
