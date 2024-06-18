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
    public class UserTreatmentUpdateValidator : AbstractValidator<UserTreatmentUpdateDTO>
    {
        public UserTreatmentUpdateValidator(MedSystemContext Context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(dto => dto.EndDate).NotEmpty();

            RuleFor(dto => dto).Must(dto => dto.EndDate > dto.StartDate)
                .OverridePropertyName("EndDate")
                .WithMessage("Treatment cannot end before starting");
        }
    }
}
