using Amazon.DynamoDBv2.DataModel;
using Hospedaria.Reservas.Api.Converters;
using System.Text.Json.Serialization;

namespace Hospedaria.Reservas.Api.Entities
{
    [DynamoDBTable("tbes_pagamentos")]
    public record Pagamento
    {
        public Pagamento()
        {
            IdPagamento = Guid.NewGuid().ToString();
            IdReserva = Descricao = string.Empty;
        }

        [DynamoDBHashKey("id_reserva")]
        [JsonPropertyName("idReserva")]
        public string IdReserva { get; set; }
        
        [JsonPropertyName("idPagamento")]
        [DynamoDBRangeKey("id_reserva")]
        public string IdPagamento { get; set; }

        [DynamoDBProperty("data", Converter = typeof(DataToStringConverter))]
        [JsonPropertyName("data")]
        public DateTime DataPagamento { get; set; }

        [DynamoDBProperty("valor")]
        [JsonPropertyName("valor")]
        public double Valor { get; set; }

        [DynamoDBProperty("descricao")]
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        public static string? GetNomeTabela() => Environment.GetEnvironmentVariable("TB_PAGAMENTOS");

    }
}
