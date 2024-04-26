using Code9WebAPI.Models;
using FluentValidation;

namespace Code9WebAPI.Validators
{
    public class AddCinemaValidator : AbstractValidator<AddCinemaRequest>
    {
        public AddCinemaValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15)
                .Must(c => !c.Any(char.IsDigit)).WithMessage("Name should not Contain any Numbers");

            RuleFor(c => c.CityId)
               .NotEmpty();

            RuleFor(c => c.NumberOfAuditoriums).GreaterThanOrEqualTo(1);
        }
    }
}
