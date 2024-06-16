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
    public class ExaminationPerformedValidator : AbstractValidator<ExaminationPerformedDTO>
    {
        public ExaminationPerformedValidator(MedSystemContext Context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(dto => dto)
                .Must(dto => Context.Examinations.Any(
                examination => examination.Id == dto.ExaminationId &&
                examination.ExaminationTerm.ExaminerId == dto.ExaminerId))
                .WithMessage("Examination with given ID does not belong to examiner")
                .WithName("ExaminationId");
        }
    }
}
