using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Code9.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext _dbContext;

        public MovieRepository(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Movie>> GetAllMovies()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);

            await _dbContext.SaveChangesAsync();

            return movie;
        }
    }
}
