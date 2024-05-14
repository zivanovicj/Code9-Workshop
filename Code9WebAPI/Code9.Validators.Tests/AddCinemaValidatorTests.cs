using AutoFixture;
using Code9WebAPI.Models;
using Code9WebAPI.Validators;
using FluentValidation.TestHelper;

namespace Code9.Validators.Tests
{
    public class AddCinemaValidatorTests
    {
        private readonly AddCinemaValidator _addCinemaValidator;
        private readonly Fixture _fixture;

        public AddCinemaValidatorTests()
        {
            _addCinemaValidator = new AddCinemaValidator();
            _fixture = new Fixture();
        }

        [Fact]
        public void AddCinemaRequest_Name_Empty()
        {
            //Arrange
            var addCinemaReqest = _fixture
                .Build<AddCinemaRequest>()
                .With(x => x.Name, "")
                .Create();

            //Act
            var result = _addCinemaValidator.TestValidate(addCinemaReqest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void AddCinemaRequest_Name_IncorrectMinLength()
        {
            //Arrange
            var addCinemaReqest = _fixture
                .Build<AddCinemaRequest>()
                .Create();

            addCinemaReqest.Name = _fixture.Create<string>().Substring(0, 2);

            //Act
            var result = _addCinemaValidator.TestValidate(addCinemaReqest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public void AddCinemaRequest_Name_IncorrectMaxLength()
        {
            //Arrange
            var addCinemaReqest = _fixture
                .Build<AddCinemaRequest>()
                .Create();

            addCinemaReqest.Name = _fixture.Create<string>().Substring(0, 20);

            //Act
            var result = _addCinemaValidator.TestValidate(addCinemaReqest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void AddCinemaRequest_CityId_Empty()
        {
            //Arrange
            var addCinemaReqest = _fixture
                .Build<AddCinemaRequest>()
                .Without(x => x.CityId)
                .Create();

            //Act
            var result = _addCinemaValidator.TestValidate(addCinemaReqest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CityId);
        }

        [Fact]
        public void AddCinemaRequest_NumberOfAuditoriums_Zero()
        {
            //Arrange
            var addCinemaReqest = _fixture
                .Build<AddCinemaRequest>()
                .With(x => x.NumberOfAuditoriums, 0)
                .Create();

            //Act
            var result = _addCinemaValidator.TestValidate(addCinemaReqest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.NumberOfAuditoriums);
        }
    }
}