using Code9.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Code9.Infrastructure
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Movies);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}