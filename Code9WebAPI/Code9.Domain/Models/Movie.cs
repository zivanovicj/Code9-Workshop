namespace Code9.Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public int Rating { get; set; }
        public ICollection<Genre> Genres { get; } = new List<Genre>();
    }
}
