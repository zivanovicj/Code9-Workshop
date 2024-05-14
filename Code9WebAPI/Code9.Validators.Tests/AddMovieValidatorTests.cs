using AutoFixture;
using Code9WebAPI.Models;
using Code9WebAPI.Validators;
using FluentValidation.TestHelper;

namespace Code9.Validators.Tests
{
    public class AddMovieValidatorTests
    {
        private readonly AddMovieValidator _addMovieValidator;
        private readonly Fixture _fixture;

        public AddMovieValidatorTests()
        {
            _addMovieValidator = new AddMovieValidator();
            _fixture = new Fixture();
        }

        [Fact]
        public void AddMovieRequest_Title_Empty()
        {
            //Arrange
            var addMovieRequest = _fixture
                .Build<AddMovieRequest>()
                .Without(x => x.Title)
                .Create();

            //Act
            var result = _addMovieValidator.TestValidate(addMovieRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void AddMovieRequest_Title_IncorrectLength()
        {
            //Arrange
            var addMovieRequest = _fixture
                .Build<AddMovieRequest>()
                .Create();

            addMovieRequest.Title = _fixture.Create<string>().Substring(0, 1);

            //Act
            var result = _addMovieValidator.TestValidate(addMovieRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void AddMovieRequest_Year_IncorrectLength()
        {
            //Arrange
            var addMovieRequest = _fixture
                .Build<AddMovieRequest>()
                .Create();

            addMovieRequest.ReleaseYear = _fixture.Create<string>().Substring(0, 1);

            //Act
            var result = _addMovieValidator.TestValidate(addMovieRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ReleaseYear);
        }

        [Fact]
        public void AddMovieRequest_Year_WrongFormat()
        {
            //Arrange
            var addMovieRequest = _fixture
                .Build<AddMovieRequest>()
                .Create();

            //Act
            var result = _addMovieValidator.TestValidate(addMovieRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ReleaseYear);
        }

        [Fact]
        public void AddMovieRequest_Rating_IncorrectMinValue()
        {
            //Arrange
            var addMovieRequest = _fixture
                .Build<AddMovieRequest>()
                .With(x => x.Rating, 0)
                .Create();

            //Act
            var result = _addMovieValidator.TestValidate(addMovieRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Rating);
        }

        [Fact]
        public void AddMovieRequest_Rating_IncorrectMaxValue()
        {
            //Arrange
            var addMovieRequest = _fixture
                .Build<AddMovieRequest>()
                .With(x => x.Rating, 6)
                .Create();

            //Act
            var result = _addMovieValidator.TestValidate(addMovieRequest);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Rating);
        }
    }
}
