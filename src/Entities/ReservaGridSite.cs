using System.Text.Json.Serialization;

namespace Hospedaria.Reservas.Api.Entities
{
    public record ReservaGridSite
    {
        public ReservaGridSite(DateTime data, List<Reserva> reservas)
        {
            Data = data;
            Reservas = reservas;
        }

        [JsonPropertyName("data")]
        public DateTime Data { get; set; }

        [JsonPropertyName("reservas")]
        public List<Reserva> Reservas { get; set; }
    }
}
