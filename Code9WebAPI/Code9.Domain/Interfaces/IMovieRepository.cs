using Code9.Domain.Models;

namespace Code9.Domain.Interfaces
{
    public interface IMovieRepository
    {
        public Task<List<Movie>> GetAllMovies();
        public Task<Movie> AddMovie(Movie movie);
    }
}
