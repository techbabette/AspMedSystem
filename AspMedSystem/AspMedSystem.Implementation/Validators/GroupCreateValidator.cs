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
    public class GroupCreateValidator : AbstractValidator<GroupCreateDTO>
    {
        private MedSystemContext _context;
        public GroupCreateValidator(MedSystemContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(groupDto => groupDto.Name).NotNull()
                                .WithMessage("Category name is required.")
                                .MinimumLength(3)
                                .WithMessage("Minimum number of characters is 3.")
                                .MaximumLength(50)
                                .WithMessage("Maximum number of characters is 50.")
                                .Must(name => !_context.Groups.Any(group => group.Name == name))
                                .WithMessage("Group name is in use.");

            RuleFor(groupDto => groupDto.DefaultRegister)
                .Must(defaultRegister => defaultRegister == false || !_context.Groups.Any(group => group.DefaultRegister == true));

            RuleFor(groupDto => groupDto.AllowedUseCases)
                .Must(useCases => useCases.All(useCase => UseCaseInfo.AllUseCases.Contains(useCase, StringComparer.CurrentCultureIgnoreCase)))
                .WithMessage("Use case does not exist");

            RuleFor(groupDto => groupDto.DisallowedUseCases)
                .Must(useCases => useCases.All(useCase => UseCaseInfo.AllUseCases.Contains(useCase, StringComparer.CurrentCultureIgnoreCase)))
                .WithMessage("Use case does not exist");
        }
    }
}
