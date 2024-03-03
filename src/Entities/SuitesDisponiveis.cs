namespace Hospedaria.Reservas.Api.Entities
{
    public static class SuitesDisponiveis
    {
        public static readonly List<Suite> Suites = new()
        {
            new(1,"Amarela"),
            new(2,"Vermelha"),
            new(3,"Azul"),
            new(4,"Verde"),
            new(5,"Chalé"),
            new(6,"Rosa")
        };

        public static readonly Dictionary<int, string> DeParaMensagemExportacao = new()
        {
            { 1,"Master" },
            { 2, "Tradicional" },
            { 3, "Tradicional" },
            { 4, "Tradicional" },
            { 5, "Chalé" },
            { 6, "Tradicional" }
        };
    }
}
