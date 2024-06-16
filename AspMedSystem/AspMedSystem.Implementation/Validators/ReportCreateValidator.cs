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
    public class ReportCreateValidator : AbstractValidator<ReportCreateDTO>
    {
        public ReportCreateValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(dto => dto.Text)
                .NotEmpty()
                .MinimumLength(20)
                .WithMessage("Report content cannot be shorter than twenty characters");
        }
    }
}
