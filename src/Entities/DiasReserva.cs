namespace Hospedaria.Reservas.Api.Entities
{
    public class DiasReserva : List<DiaReserva>
    {
        public List<string> BuscarReservas()
        {
            if (this.Any())
            {
                List<string> idReservas = new();
                ForEach((dia) =>
                {
                    if(!idReservas.Exists(c => c == dia.IdReserva))
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
