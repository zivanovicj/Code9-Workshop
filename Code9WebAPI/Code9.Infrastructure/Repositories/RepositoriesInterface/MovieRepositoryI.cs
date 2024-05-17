using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using Code9.Infrastructure.Interfaces;

namespace Code9.Infrastructure.Repositories.RepositoriesInterface
{
    public class MovieRepositoryI : IMovieRepository
    {
        private readonly IDbContext _dbContext;
        public MovieRepositoryI(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            return await _dbContext.AddEntity<Movie>(movie);
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _dbContext.GetEntities<List<Movie>>();
        }
    }
}
