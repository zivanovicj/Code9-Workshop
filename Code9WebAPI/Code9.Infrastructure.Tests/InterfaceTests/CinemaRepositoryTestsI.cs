using AutoFixture;
using Code9.Domain.Models;
using Code9.Infrastructure.Interfaces;
using Code9.Infrastructure.Repositories.RepositoriesInterface;
using FluentAssertions;
using Moq;
using Shouldly;

namespace Code9.Infrastructure.Tests.InterfaceTests
{
    public class CinemaRepositoryTestsI
    {
        private readonly Fixture _fixture;
        private readonly MockRepository _mockRepository;
        private readonly Mock<IDbContext> _dbContext;
        private readonly CinemaRepositoryI _cinemaRepository;
        private readonly Exception _exception;

        public CinemaRepositoryTestsI()
        {
            _fixture = new Fixture();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _dbContext = _mockRepository.Create<IDbContext>();
            _cinemaRepository = new CinemaRepositoryI(_dbContext.Object);
            _exception = new Exception();
        }

        [Fact]
        public async Task GetAllCinema_ShouldReturn_CinemaCollection()
        {
            //Arrange
            var city = _fixture
                .Build<City>()
                .Without(x => x.Cinema)
                .Create();
            var cinemas = _fixture
                .Build<Cinema>()
                .With(x => x.Cities, city)
                .CreateMany()
                .ToList();
            _dbContext.Setup(x => x.GetEntities<List<Cinema>>()).ReturnsAsync(cinemas);

            //Act
            var result = await _cinemaRepository.GetAllCinema();

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<List<Cinema>>();
            result.Count.Should().Be(cinemas.Count);
            result.ShouldBeEquivalentTo(cinemas);
        }

        [Fact]
        public async Task AddCinema_Should_AddCinema()
        {
            var city = _fixture
                .Build<City>()
                .Without(x => x.Cinema)
                .Create();
            var cinema = _fixture
                .Build<Cinema>()
                .With(x => x.Cities, city)
                .Create();
            _dbContext.Setup(x => x.AddEntity(cinema)).ReturnsAsync(cinema);

            //Act
            var result = await _cinemaRepository.AddCinema(cinema);

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<Cinema>();
            result.ShouldBeEquivalentTo(cinema);
        }

        [Fact]
        public void AddCinema_Should_ThrowException()
        {
            _dbContext.Setup(x => x.AddEntity(It.IsAny<Cinema>())).ThrowsAsync(_exception);

            //Act
            Func<Task> action = async () =>
            {
                await _cinemaRepository.AddCinema(It.IsAny<Cinema>());
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public async Task UpdateCinema_Should_UpdateCinema()
        {
            //Arrange
            var city = _fixture
                .Build<City>()
                .Without(x => x.Cinema)
                .Create();
            var cinema = _fixture
                .Build<Cinema>()
                .With(x => x.Cities, city)
                .Create();
            _dbContext.Setup(x => x.UpdateEntity(cinema)).ReturnsAsync(cinema);

            //Act
            var result = await _cinemaRepository.UpdateCinema(cinema);


            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<Cinema>();
            result.ShouldBeEquivalentTo(cinema);
        }

        [Fact]
        public void UpdateCinema_Should_ThrowException()
        {
            //Arrange
            _dbContext.Setup(x => x.UpdateEntity(It.IsAny<Cinema>())).ThrowsAsync(_exception);

            //Act
            Func<Task> action = async () =>
            {
                await _cinemaRepository.UpdateCinema(It.IsAny<Cinema>());
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public void DeleteCinema_Should_DeleteCinema()
        {
            //Arrange
            _dbContext.Setup(x => x.DeleteEntity(It.IsAny<Cinema>())).Returns(Task.CompletedTask);

            //Act
            Func<Task> action = async () =>
            {
                await _cinemaRepository.DeleteCinema(It.IsAny<Cinema>());
            };

            //Assert
            action.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public void DeleteCinema_Should_ThrowException()
        {
            //Arrange
            _dbContext.Setup(x => x.DeleteEntity(It.IsAny<Cinema>())).ThrowsAsync(_exception);

            //Act
            Func<Task> action = async () =>
            {
                await _cinemaRepository.DeleteCinema(It.IsAny<Cinema>());
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public async Task GetCinema_Should_ReturnCinema()
        {
            //Arrange
            var cinemaId = _fixture.Create<Guid>();
            var city = _fixture
                .Build<City>()
                .Without(x => x.Cinema)
                .Create();
            var cinema = _fixture
                .Build<Cinema>()
                .With(x => x.Cities, city)
                .With(x => x.Id, cinemaId)
                .Create();
            _dbContext.Setup(x => x.GetEntity<Cinema>(cinemaId)).ReturnsAsync(cinema);

            //Act
            var result = await _cinemaRepository.GetCinema(cinemaId);

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<Cinema>();
            result.ShouldBeEquivalentTo(cinema);
        }

        [Fact]
        public async Task GetCinema_Should_ReturnNull()
        {
            //Arrange
            var cinemaId = _fixture.Create<Guid>();
            _dbContext.Setup(x => x.GetEntity<Cinema>(cinemaId)).Returns(Task.FromResult<Cinema>(null));

            //Act
            var result = await _cinemaRepository.GetCinema(cinemaId);

            //Assert
            result.Should().BeNull();
        }
    }
}
