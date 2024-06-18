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
    public class ExaminationTermUpdateValidator : AbstractValidator<ExaminationTermUpdateDTO>
    {
        public ExaminationTermUpdateValidator(MedSystemContext Context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            var performExaminationPerm = UseCaseInfo.performExaminationPerm;

            RuleFor(dto => dto.DateTime)
                .NotEmpty()
                .WithMessage("DateTime is required");

            RuleFor(dto => dto)
                .Must(dto => !Context.ExaminationTerms.Any(
                    term => term.ExaminerId == dto.ExaminerId && term.Id != dto.Id &&
                    dto.DateTime > term.Date.AddMinutes(-15) && dto.DateTime < term.Date.AddMinutes(15)))
                .WithMessage("Cannot publish a term that's within fifteen minutes of another term for the same examiner")
                .OverridePropertyName("DateTime");

            RuleFor(dto => dto.ExaminerId)
                .Must(examinerId => Context.Users.Any(
                    user => user.Id == examinerId &&
                    ((
                            user.UserPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Allow)
                    ||
                            user.Group.GroupPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Allow)
                    )
                    && !user.UserPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Disallow
                    ))))
                .WithMessage("Cannot publish a term for a user that cannot perform examinations")
                .OverridePropertyName("ExaminerId");
        }
    }
}
