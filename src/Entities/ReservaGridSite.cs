using System.Text.Json.Serialization;

namespace Hospedaria.Reservas.Api.Entities
{
    public record ReservaGridSite
    {
        public ReservaGridSite(DateTime data, List<Reserva> reservas, string urlExportar)
        {
            Data = data;
            Reservas = reservas;
            UrlExportarWhats = urlExportar;
        }

        [JsonPropertyName("data")]
        public DateTime Data { get; set; }

        [JsonPropertyName("reservas")]
        public List<Reserva> Reservas { get; set; }

        [JsonPropertyName("urlExportar")]
        public string UrlExportarWhats { get; set; }
    }
}
