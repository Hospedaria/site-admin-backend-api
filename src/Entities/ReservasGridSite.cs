using System.Text.Json.Serialization;

namespace Hospedaria.Reservas.Api.Entities
{
    public record ReservasGridSite
    {
        [JsonPropertyName("data")]
        public DateTime Data { get; set; }

        [JsonPropertyName("reservas")]
        public List<Reserva> Reservas { get; set; }
    }
}
