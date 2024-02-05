using Amazon.DynamoDBv2.DataModel;
using Hospedaria.Reservas.Api.Converters;
using System.Text.Json.Serialization;

namespace Hospedaria.Reservas.Api.Entities
{
    [DynamoDBTable("tbes_dias_reservas_dev")]
    public record DiaReserva
    {
        public DiaReserva(string id, DateTime dataReferencia)
        {
            IdReserva = id;
            Data = dataReferencia;
        }

        [DynamoDBHashKey("data", Converter = typeof(DataToStringConverter))]
        [JsonPropertyName("data")]
        public DateTime Data { get; set; }

        [DynamoDBRangeKey("id")]
        [JsonPropertyName("idReserva")]
        public string IdReserva { get; set; }

        public static string GetNomeTabela()
        {
            string? nomeTabela = Environment.GetEnvironmentVariable("TB_DIAS_RESERVAS");
            nomeTabela ??= string.Empty;

            return nomeTabela;
        }
    }
}
