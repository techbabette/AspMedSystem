using AspMedSystem.Application.DTO;
using AspMedSystem.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.Validators
{
    public class AuthRegisterValidator : AbstractValidator<AuthRegisterDTO>
    {
        public AuthRegisterValidator(MedSystemContext Context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(dto => dto.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(dtoEmail => !Context.Users.Any(user => user.Email == dtoEmail))
                .WithMessage("Email is already in use.");

            RuleFor(dto => dto.FirstName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(dto => dto.LastName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(dto => dto.Password)
                .NotEmpty()
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$")
                .WithMessage("Minimum eight characters, at least one uppercase letter, one lowercase letter and one number:");
            
            RuleFor(dto => dto.BirthDate)
                .NotEmpty()
                .Must(dtoBirthday => (DateTime.UtcNow - dtoBirthday.Value).TotalDays > (12 * 365))
                .WithMessage("You have to be at least 12 years old.");
        }
    }
}
