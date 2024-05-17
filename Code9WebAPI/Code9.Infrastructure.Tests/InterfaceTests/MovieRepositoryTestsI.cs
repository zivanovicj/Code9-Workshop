using AutoFixture;
using Code9.Domain.Models;
using Code9.Infrastructure.Interfaces;
using Code9.Infrastructure.Repositories.RepositoriesInterface;
using FluentAssertions;
using Moq;
using Shouldly;

namespace Code9.Infrastructure.Tests.InterfaceTests
{
    public class MovieRepositoryTestsI
    {
        private readonly Fixture _fixture;
        private readonly MockRepository _mockRepository;
        private readonly Mock<IDbContext> _dbContext;
        private readonly MovieRepositoryI _movieRepository;

        public MovieRepositoryTestsI()
        {
            _fixture = new Fixture();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _dbContext = _mockRepository.Create<IDbContext>();
            _movieRepository = new MovieRepositoryI(_dbContext.Object);
        }

        [Fact]
        public async Task GetAllMovies_ShouldReturn_MovieCollection()
        {
            //Arrange
            var movies = _fixture.CreateMany<Movie>().ToList();
            _dbContext.Setup(x => x.GetEntities<List<Movie>>()).ReturnsAsync(movies);

            //Act
            var result = await _movieRepository.GetAllMovies();

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<List<Movie>>();
            result.Count.ShouldBe(movies.Count);
            result.ShouldBeEquivalentTo(movies);
        }

        [Fact]
        public async Task AddMovie_Should_AddMovie()
        {
            //Arrange
            var movie = _fixture.Create<Movie>();
            _dbContext.Setup(x => x.AddEntity<Movie>(movie)).ReturnsAsync(movie);

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
            var movie = _fixture.Create<Movie>();

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
