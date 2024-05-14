using AutoFixture;
using Code9.Domain.Commands;
using Code9.Domain.Handlers;
using Code9.Domain.Interfaces;
using Code9.Domain.Models;
using Code9.Domain.Queries;
using FluentAssertions;
using Moq;
using Shouldly;

namespace Code9.Handlers.Tests.CinemaHandlers
{
    public class CinemaHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICinemaRepository> _mockCinemaRepository;
        private readonly AddCinemaHandler _addCinemaHandler;
        private readonly MockRepository _mockRepository;
        private readonly DeleteCinemaHandler _deleteCinemaHandler;
        private readonly UpdateCinemaHandler _updateCinemaHandler;
        private readonly GetAllCinemaHandler _getAllCinemaHandler;

        public CinemaHandlerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);
            _fixture = new Fixture();
            _mockCinemaRepository = _mockRepository.Create<ICinemaRepository>();
            _addCinemaHandler = new AddCinemaHandler(_mockCinemaRepository.Object);
            _deleteCinemaHandler = new DeleteCinemaHandler(_mockCinemaRepository.Object);
            _updateCinemaHandler = new UpdateCinemaHandler(_mockCinemaRepository.Object);
            _getAllCinemaHandler = new GetAllCinemaHandler(_mockCinemaRepository.Object);
        }

        [Fact]
        public async void AddCinemaHandler_OKTest()
        {
            //Arrange
            var addCinemaCommand = _fixture.Create<AddCinemaCommand>();
            var cinema = _fixture
                .Build<Cinema>()
                .Without(x => x.Cities)
                .Create();
            _mockCinemaRepository.Setup(x => x.AddCinema(It.IsAny<Cinema>())).ReturnsAsync(cinema);

            //Act
            var result = await _addCinemaHandler.Handle(addCinemaCommand, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(cinema);
            result.Should().BeOfType(typeof(Cinema));
        }

        [Fact]
        public void AddCinemaHandler_ThrowsException()
        {
            //Arrange
            var addCinemaCommand = _fixture.Create<AddCinemaCommand>();
            var exception = _fixture.Create<Exception>();
            _mockCinemaRepository.Setup(x => x.AddCinema(It.IsAny<Cinema>())).ThrowsAsync(exception);

            //Act
            Func<Task> action = async () =>
            {
                await _addCinemaHandler.Handle(addCinemaCommand, CancellationToken.None);
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public void DeleteCinemaHandler_IdNotFound()
        {
            //Arrange
            var deleteCinemaCommand = _fixture.Create<DeleteCinemaCommand>();
            var cinemaId = _fixture.Create<Guid>();
            _mockCinemaRepository.Setup(x => x.GetCinema(cinemaId)).Returns(Task.FromResult<Cinema>(null));

            //Act
            Func<Task> action = async () =>
            {
                await _deleteCinemaHandler.Handle(deleteCinemaCommand, CancellationToken.None);
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public void UpdateCinemaHandler_IdNotFound()
        {
            //Arrange
            var updateCinemaCommand = _fixture.Create<UpdateCinemaCommand>();
            var cinemaId = _fixture.Create<Guid>();
            _mockCinemaRepository.Setup(x => x.GetCinema(cinemaId)).Returns(Task.FromResult<Cinema>(null));

            //Act
            Func<Task> action = async () =>
            {
                await _updateCinemaHandler.Handle(updateCinemaCommand, CancellationToken.None);
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public async void UpdateCinemaHandler_OkTest()
        {
            //Arrange
            var updateCinemaCommand = _fixture.Create<UpdateCinemaCommand>();
            var cinema = _fixture
                .Build<Cinema>()
                .Without(x => x.Cities)
                .Create();
            _mockCinemaRepository.Setup(x => x.GetCinema(updateCinemaCommand.Id)).ReturnsAsync(cinema);
            _mockCinemaRepository.Setup(x => x.UpdateCinema(cinema)).ReturnsAsync(cinema);

            //Act
            var result = await _updateCinemaHandler.Handle(updateCinemaCommand, CancellationToken.None);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeEquivalentTo(cinema);
        }

        [Fact]
        public async void GetAllCinemaHandler_ReturnsCinemaCollection()
        {
            //Arrange
            var expectedResultCount = 5;
            var getAllCinemaQuery = _fixture.Create<GetAllCinemaQuery>();
            var cinemaCollection = _fixture
                .Build<Cinema>()
                .Without(x => x.Cities)
                .CreateMany(5)
                .ToList();
            _mockCinemaRepository.Setup(x => x.GetAllCinema()).ReturnsAsync(cinemaCollection);

            //Act
            var result = await _getAllCinemaHandler.Handle(getAllCinemaQuery, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Cinema>>();
            result.Should().HaveCount(expectedResultCount);
            result.ShouldBeEquivalentTo(cinemaCollection);
        }
    }
}
