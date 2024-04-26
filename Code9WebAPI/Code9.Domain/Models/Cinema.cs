namespace Code9.Domain.Models
{
    public class Cinema
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int NumberOfAuditoriums { get; set; }
        public City Cities { get; set; }
    }
}