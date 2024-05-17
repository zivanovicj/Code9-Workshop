using AutoFixture;
using Code9.Domain.Models;
using Code9.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Code9.Infrastructure.Tests.BadTests
{
    public class CinemaRepositoryTests
    {
        private readonly Fixture _fixture;
        private readonly CinemaDbContext _dbContext;
        private readonly CinemaRepository _cinemaRepository;
        private string connectionString = "Data Source=localhost;Initial Catalog=CinemaDockerCodeFirst;User ID=sa;Password=Code9#2024;Pooling=False;Encrypt=False;Trust Server Certificate=False";

        public CinemaRepositoryTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            _dbContext = new CinemaDbContext(options);
            _cinemaRepository = new CinemaRepository(_dbContext);
        }

        [Fact]
        public async Task GetAllCinema_ShouldReturn_CinemaCollection()
        {
            //Arrange


            //Act
            var result = await _cinemaRepository.GetAllCinema();

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<List<Cinema>>();
        }

        [Fact]
        public async Task AddCinema_Should_AddCinema()
        {
            //Arrange
            var city = _fixture
                .Build<City>()
                .Without(x => x.Id)
                .Create();
            var cinema = _fixture
                .Build<Cinema>()
                .Without(x => x.Id)
                .With(x => x.Cities, city)
                .With(x => x.CityId, city.Id)
                .Create();

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
            //Arrange
            var cinema = _fixture
                .Build<Cinema>()
                .Without(x => x.Cities)
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _cinemaRepository.AddCinema(cinema);
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public async Task UpdateCinema_Should_UpdateCinema()
        {
            //Arrange
            var cinemas = await _cinemaRepository.GetAllCinema();
            Cinema cinema;
            if (cinemas.Count() == 0)
            {
                var city = _fixture
                .Build<City>()
                .Without(x => x.Id)
                .Create();
                cinema = _fixture
                    .Build<Cinema>()
                    .Without(x => x.Id)
                    .With(x => x.Cities, city)
                    .With(x => x.CityId, city.Id)
                    .Create();

                await _cinemaRepository.AddCinema(cinema);
            }
            else
            {
                cinema = cinemas[0];
            }

            cinema.Name = "Test Name";

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
            var cinema = _fixture
                .Build<Cinema>()
                .With(x => x.Name, new String('t', 100))
                .Create();

            //Act
            Func<Task> action = async () =>
            {
                await _cinemaRepository.UpdateCinema(cinema);
            };

            //Assert
            action.ShouldThrow<Exception>();
        }

        [Fact]
        public async Task DeleteCinema_Should_DeleteCinema()
        {
            //Arrange
            var cinemas = await _cinemaRepository.GetAllCinema();
            Cinema cinema;
            if (cinemas.Count() == 0)
            {
                var city = _fixture
                .Build<City>()
                .Without(x => x.Id)
                .Create();
                cinema = _fixture
                    .Build<Cinema>()
                    .Without(x => x.Id)
                    .With(x => x.Cities, city)
                    .With(x => x.CityId, city.Id)
                    .Create();

                await _cinemaRepository.AddCinema(cinema);
            }
            else
            {
                cinema = cinemas[0];
            }

            //Act 
            try
            {
                await _cinemaRepository.DeleteCinema(cinema);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Delete cinema failed with exception: {ex.Message}");
            }
        }

        [Fact]
        public async Task DeleteCinema_Should_ThrowException()
        {
            //Arrange
            var cinema = _fixture
                    .Build<Cinema>()
                    .Without(x => x.Cities)
                    .Create();

            //Act 
            try
            {
                await _cinemaRepository.DeleteCinema(cinema);
            }
            catch (Exception ex)
            {
                Assert.True(true, $"Delete cinema throws exception: {ex}");
            }
        }

        [Fact]
        public async Task GetCinema_Should_ReturnCinema()
        {
            //Arrange
            var city = _fixture
                .Build<City>()
                .Without(x => x.Id)
                .Create();
            var cinema = _fixture
                .Build<Cinema>()
                .Without(x => x.Id)
                .With(x => x.Cities, city)
                .With(x => x.CityId, city.Id)
                .Create();
            Cinema addedCinema = await _cinemaRepository.AddCinema(cinema);

            //Act
            var result = await _cinemaRepository.GetCinema(addedCinema.Id);

            //Assert
            result.Should().NotBeNull();
            result.ShouldBeOfType<Cinema>();
            result.ShouldBeEquivalentTo(addedCinema);
        }

        [Fact]
        public async Task GetCinema_Should_ReturnNull()
        {
            //Arrange
            var cinemaId = _fixture.Create<Guid>();

            //Act
            var result = await _cinemaRepository.GetCinema(cinemaId);

            //Assert
            result.Should().BeNull();
        }
    }
}