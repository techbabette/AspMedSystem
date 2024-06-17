using AspMedSystem.Application.DTO;
using AspMedSystem.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.Validators
{
    public class TreatmentUpdateValidator : AbstractValidator<TreatmentUpdateDTO>
    {
        public TreatmentUpdateValidator(MedSystemContext Context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(treatmentDto => treatmentDto.Name).NotNull()
                                .WithMessage("Treatment name is required.")
                                .MinimumLength(3)
                                .WithMessage("Minimum number of characters is 3.")
                                .MaximumLength(50)
                                .WithMessage("Maximum number of characters is 50.")
                                .Must(name => !Context.Treatments.Any(group => group.Name == name))
                                .WithMessage("Treatment name is in use.");

            RuleFor(groupDto => groupDto)
                .Must(groupDto => !Context.Groups.Any(group => group.Name == groupDto.Name && group.Id != groupDto.Id))
                .OverridePropertyName("Name")
                .WithMessage("Group name is in use.");
        }
    }
}
