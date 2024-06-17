using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Treatments;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Treatments
{
    public class EfTreatmentCreateCommand : EfUseCase, ITreatmentCreateCommand
    {
        private readonly TreatmentCreateValidator validator;

        private EfTreatmentCreateCommand()
        {
        }

        public EfTreatmentCreateCommand(MedSystemContext context, TreatmentCreateValidator validator) : base(context)
        {
            this.validator = validator;
        }

        public string Name => "Create treatment";
        public void Execute(TreatmentCreateDTO data)
        {
            validator.ValidateAndThrow(data);

            Context.Treatments.Add(new Domain.Treatment
            {
                Name = data.Name,
                Prescribable = data.Prescribable
            });
            Context.SaveChanges();
        }
    }
}
