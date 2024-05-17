using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using Code9.Infrastructure.Interfaces;

namespace Code9.Infrastructure.Repositories.RepositoriesInterface
{
    public class CinemaRepositoryI : ICinemaRepository
    {
        private readonly IDbContext _dbContext;

        public CinemaRepositoryI(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cinema> AddCinema(Cinema cinema)
        {
            return await _dbContext.AddEntity<Cinema>(cinema);
        }

        public async Task DeleteCinema(Cinema cinema)
        {
            await _dbContext.DeleteEntity<Cinema>(cinema);
        }

        public async Task<List<Cinema>> GetAllCinema()
        {
            return await _dbContext.GetEntities<List<Cinema>>();
        }

        public async Task<Cinema> GetCinema(Guid id)
        {
            return await _dbContext.GetEntity<Cinema>(id);
        }

        public async Task<Cinema> UpdateCinema(Cinema cinema)
        {
            return await _dbContext.UpdateEntity<Cinema>(cinema);
        }
    }
}
