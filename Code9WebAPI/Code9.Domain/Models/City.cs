namespace Code9.Domain.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        // Collection navigation containing dependents
        public ICollection<Cinema> Cinema { get; set; } = new List<Cinema>();
    }
}
