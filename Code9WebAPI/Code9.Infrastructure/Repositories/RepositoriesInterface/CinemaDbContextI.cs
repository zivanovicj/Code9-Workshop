using Code9.Domain.Models;
using Code9.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Code9.Infrastructure.Repositories.RepositoriesInterface
{
    public class CinemaDbContextI : DbContext, IDbContext
    {
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public CinemaDbContextI(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContextI).Assembly);
        }

        public Task<T> AddEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntities<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntity<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateEntity<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
