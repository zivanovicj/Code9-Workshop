using AutoFixture;
using Code9.Domain.Commands;
using Code9.Domain.Handlers;
using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using Code9.Domain.Queries;
using FluentAssertions;
using Moq;
using Shouldly;

namespace Code9.Handlers.Tests.MovieHandlers
{
    public class MovieHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly MockRepository _mockRepository;
        private readonly AddMovieHandler _addMovieHandler;
        private readonly GetAllMoviesHandler _getAllMoviesHandler;

        public MovieHandlerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);
            _fixture = new Fixture();
            _mockMovieRepository = _mockRepository.Create<IMovieRepository>();
            _addMovieHandler = new AddMovieHandler(_mockMovieRepository.Object);
            _getAllMoviesHandler = new GetAllMoviesHandler(_mockMovieRepository.Object);
        }

        [Fact]
        public async void AddMovieHandler_OkTest()
        {
            //Arrange
            var addMovieCommand = _fixture.Create<AddMovieCommand>();
            var movie = _fixture.Create<Movie>();
            _mockMovieRepository.Setup(x => x.AddMovie(It.IsAny<Movie>())).ReturnsAsync(movie);

            //Act
            var result = await _addMovieHandler.Handle(addMovieCommand, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<Movie>();
            result.ShouldBeEquivalentTo(movie);
        }

        [Fact]
        public void AddMovieHandler_ThrowsException()
        {
            //Arrange
            var addMovieCommand = _fixture.Create<AddMovieCommand>();
            var exception = _fixture.Create<Exception>();
            _mockMovieRepository.Setup(x => x.AddMovie(It.IsAny<Movie>())).ThrowsAsync(exception);

            //Act
            Func<Task> action = async () =>
            {
                await _addMovieHandler.Handle(addMovieCommand, CancellationToken.None);
            };

            //Assert
            action.ShouldThrow<Exception>();
        }
        [Fact]
        public async void GetAllMoviesHandler_ReturnsMovieCollection()
        {
            //Arrange
            var expectedResultCount = 5;
            var getAllMoviesQuery = _fixture.Create<GetAllMoviesQuery>();
            var movieCollection = _fixture
                .CreateMany<Movie>(5)
                .ToList();
            _mockMovieRepository.Setup(x => x.GetAllMovies()).ReturnsAsync(movieCollection);

            //Act
            var result = await _getAllMoviesHandler.Handle(getAllMoviesQuery, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Movie>>();
            result.Should().HaveCount(expectedResultCount);
            result.ShouldBeEquivalentTo(movieCollection);
        }
    }
}
