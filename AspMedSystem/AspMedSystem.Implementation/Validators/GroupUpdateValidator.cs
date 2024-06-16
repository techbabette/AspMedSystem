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
    public class GroupUpdateValidator : AbstractValidator<GroupUpdateDTO>
    {
        private MedSystemContext _context;
        public GroupUpdateValidator(MedSystemContext context)
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(groupDto => groupDto.Name).NotNull()
                                .WithMessage("Category name is required.")
                                .MinimumLength(3)
                                .WithMessage("Minimum number of characters is 3.")
                                .MaximumLength(50)
                                .WithMessage("Maximum number of characters is 50.");


            RuleFor(groupDto => groupDto)
                .Must(groupDto => !_context.Groups.Any(group => group.Name == groupDto.Name && group.Id != groupDto.Id))
                .WithName("Name")
                .WithMessage("Group name is in use.");

            RuleFor(groupDto => groupDto)
                .Must(groupDto => groupDto.DefaultRegister == false || !_context.Groups.Any(group => group.DefaultRegister == true && group.Id != groupDto.Id))
                .WithName("DefaultRegister")
                .WithMessage("Only one group can be set as default for newly registered users");

            RuleForEach(groupDto => groupDto.AllowedUseCases).Must((groupDto, useCase) =>
            {
                return UseCaseInfo.AllUseCases.Contains(useCase, StringComparer.CurrentCultureIgnoreCase);
            }).WithMessage("Use case does not exist");
        }
    }
}
