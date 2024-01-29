using Amazon.DynamoDBv2.DataModel;
using Hospedaria.Reservas.Api.Enums;
using System.Text.Json.Serialization;

namespace Hospedaria.Reservas.Api.Entities
{
    [DynamoDBTable("tbes_reservas_dev")]
    public record Reserva
    {
        public Reserva()
        {
            Email = Telefone = ChegaraAs = Observacoes = Nome = string.Empty;
            Suites = new();
            Id = Guid.NewGuid().ToString();
        }

        [DynamoDBHashKey("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [DynamoDBProperty("nome")]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        
        [DynamoDBProperty("telefone")]
        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }
        
        [DynamoDBProperty("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [DynamoDBProperty("checkin")]
        [JsonPropertyName("checkin")]
        [DynamoDBGlobalSecondaryIndexHashKey("ix_checkin", AttributeName = "checkin")]
        public DateTime? CheckIn { get; set; }
        
        [DynamoDBProperty("checkout")]
        [JsonPropertyName("checkout")]
        public DateTime? CheckOut { get; set; }

        [DynamoDBProperty("chegada")]
        [JsonPropertyName("chegada")]
        public string ChegaraAs { get; set; }
        
        [DynamoDBProperty("valor")]
        [JsonPropertyName("valor")]
        public double Valor { get; set; }
        
        [DynamoDBProperty("qtdAdultos")]
        [JsonPropertyName("qtdAdultos")]

        public short QuantidadeAdultos { get; set; }
        
        [DynamoDBProperty("qtdCriancas")]
        [JsonPropertyName("qtdCriancas")]
        public short QuantidadeCriancas { get; set; }
        
        [DynamoDBProperty("status")]
        [JsonPropertyName("status")]
        public EStatusReserva StatusReserva { get; set; }
        
        [DynamoDBProperty("observacoes")]
        [JsonPropertyName("observacoes")]
        public string Observacoes { get; set; }
        
        [DynamoDBProperty("suites")]
        [JsonPropertyName("suites")]
        public List<int> Suites { get; set; }

        public static string? GetNomeTabela() => Environment.GetEnvironmentVariable("TB_RESERVAS");
    }
}
