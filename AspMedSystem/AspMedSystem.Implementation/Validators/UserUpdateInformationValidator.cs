using AspMedSystem.Application.DTO;
using AspMedSystem.DataAccess;
using FluentValidation;

namespace AspMedSystem.Implementation.Validators
{
    public class UserUpdateInformationValidator : AbstractValidator<UserUpdateInformationDTO>
    {
        public UserUpdateInformationValidator(MedSystemContext Context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(dto => dto.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email must be valid email address.");

            RuleFor(dto => dto)
                .Must(dto => !Context.Users.Any(user => user.Id != dto.Id && user.Email == dto.Email))
                .WithMessage("Email is already in use")
                .WithName("Email");

            RuleFor(dto => dto.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .WithMessage("First name should be between two and fifty characters long");

            RuleFor(dto => dto.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .WithMessage("Last name should be between two and fifty characters long");
        }
    }
}
