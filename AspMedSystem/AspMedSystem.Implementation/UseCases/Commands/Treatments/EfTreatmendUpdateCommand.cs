using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
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
    public class EfTreatmendUpdateCommand : EfUseCase, ITreatmentUpdateCommand
    {
        private readonly TreatmentUpdateValidator validator;

        private EfTreatmendUpdateCommand()
        {
        }

        public EfTreatmendUpdateCommand(MedSystemContext context, TreatmentUpdateValidator validator) : base(context)
        {
            this.validator = validator;
        }

        public string Name => "Update treatment";

        public void Execute(TreatmentUpdateDTO data)
        {
            validator.ValidateAndThrow(data);
            
            var treatmentToUpdate = Context.Treatments.Find(data.Id);

            if (treatmentToUpdate == null)
            {
                throw new EntityNotFoundException("Treatment", data.Id);
            }

            treatmentToUpdate.Prescribable = data.Prescribable;
            treatmentToUpdate.Name = data.Name;
            Context.SaveChanges();
        }
    }
}
