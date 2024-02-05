using Hospedaria.Reservas.Api.Entities;

namespace Hospedaria.Reservas.Api.Extensions
{
    public static class DiaReservaExtensions
    {
        public static List<string> BuscarReservas(this List<DiaReserva> dias)
        {
            if (dias.Any())
            {
                List<string> idReservas = new();
                dias.ForEach((dia) =>
                {
                    if (!idReservas.Exists(c => c == dia.IdReserva))
                    {
                        idReservas.Add(dia.IdReserva);
                    }
                });

                return idReservas;
            }
            return new List<string>();
        }
    }
}
