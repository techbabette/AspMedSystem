using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Treatments;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Treatments
{
    public class EfTreatmentDeleteCommand : EfUseCase, ITreatmentDeleteCommand
    {
        private EfTreatmentDeleteCommand()
        {
        }

        public EfTreatmentDeleteCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Delete treatment";

        public void Execute(int data)
        {
            var treatmentToDelete = Context.Treatments.Where(treatment => treatment.Id == data)
                                                      .Include(treatment => treatment.UserTreatments)
                                                      .FirstOrDefault();

            if (treatmentToDelete == null)
            {
                throw new EntityNotFoundException("Treatment", data);
            }

            if (treatmentToDelete.UserTreatments.Any())
            {
                throw new ConflictException("Cannot delete a treatment that was prescribed to user, try making it unprescribable instead");
            }

            Context.Treatments.Remove(treatmentToDelete);
            Context.SaveChanges();
        }
    }
}
