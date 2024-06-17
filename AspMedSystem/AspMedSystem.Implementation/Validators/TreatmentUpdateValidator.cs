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
                                .WithMessage("Maximum number of characters is 50.");

            RuleFor(treatmentDto => treatmentDto)
                .Must(treatmentDto => !Context.Treatments.Any(treatment => treatment.Name == treatmentDto.Name && treatment.Id != treatmentDto.Id))
                .OverridePropertyName("Name")
                .WithMessage("Treatment name is in use.");

            RuleFor(treatmentDto => treatmentDto.Prescribable).NotNull();
        }
    }
}
