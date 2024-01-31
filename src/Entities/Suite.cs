namespace Hospedaria.Reservas.Api.Entities
{
    public record Suite
    {
        public Suite() : this(0, string.Empty) { }

        public Suite(int id, string cor)
        {
            Id = id;
            Cor = cor;
        }

        public int Id { get; set; }
        public string Cor { get; set; }
    }
}
