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
    public class UserUpdatePermissionsValidator : AbstractValidator<UserUpdatePermissionsDTO>
    {
        public UserUpdatePermissionsValidator(MedSystemContext Context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleForEach(dto => dto.AllowedUseCases).Must((dto, useCase) =>
            {
                return UseCaseInfo.AllUseCases.Contains(useCase, StringComparer.CurrentCultureIgnoreCase);
            }).WithMessage("Use case does not exist");

            RuleForEach(dto => dto.DisallowedUseCases).Must((dto, useCase) =>
            {
                return UseCaseInfo.AllUseCases.Contains(useCase, StringComparer.CurrentCultureIgnoreCase);
            }).WithMessage("Use case does not exist");
        }
    }
}
