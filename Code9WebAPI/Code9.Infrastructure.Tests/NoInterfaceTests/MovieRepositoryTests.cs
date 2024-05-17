using AutoFixture;
using Code9.Domain.Models;
using Code9.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Code9.Infrastructure.Tests.BadTests
{
    public class MovieRepositoryTests
    {
        private readonly Fixture _fixture;
        private readonly CinemaDbContext _dbContext;
        private readonly MovieRepository _movieRepository;
        private string connectionString = "Data Source=localhost;Initial Catalog=CinemaDockerCodeFirst;User ID=sa;Password=Code9#2024;Pooling=False;Encrypt=False;Trust Server Certificate=False";

        public MovieRepositoryTests()
        {
            _fixture = new Fixture();
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            _dbContext = new CinemaDbContext(options);
            _movieRepository = new MovieRepository(_dbContext);
        }

        [Fact]
        public async Task GetAllMovies_ShouldReturn_MovieCollection()
        {
            //Arrange


            //Act
            var result = await _movieRepository.GetAllMovies();

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<List<Movie>>();
        }

        [Fact]
        public async Task AddMovie_Should_AddMovie()
        {
            //Arrange
            var movie = _fixture
                .Build<Movie>()
                .Without(x => x.Id)
                .With(x => x.ReleaseYear, "2024")
                .Create();

            //Act
            var result = await _movieRepository.AddMovie(movie);

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<Movie>();
            result.ShouldBeEquivalentTo(movie);
        }

        [Fact]
        public void AddMovie_Should_ThrowException()
        {
            //Arrange
            var movie = _fixture
                .Build<Movie>()
                .With(x => x.ReleaseYear, "2024")
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _movieRepository.AddMovie(movie);
            };

            //Assert
            action.ShouldThrow<Exception>();
        }
    }
}
