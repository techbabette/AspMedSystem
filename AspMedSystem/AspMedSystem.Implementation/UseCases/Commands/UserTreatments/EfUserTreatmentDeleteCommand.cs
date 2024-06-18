using AspMedSystem.Application;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.UserTreatments;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.UserTreatments
{
    public class EfUserTreatmentDeleteCommand : EfUseCase, IUserTreatmentDeleteCommand
    {
        private readonly IApplicationActor actor;

        private EfUserTreatmentDeleteCommand()
        {
        }

        public EfUserTreatmentDeleteCommand(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Stop treatment";

        public void Execute(int data)
        {
            var prescriptionToDelete = Context.UserTreatments.Where(tr => tr.Id == data)
                                                             .Include(tr => tr.Report)
                                                             .ThenInclude(report => report.Examination)
                                                             .ThenInclude(examination => examination.ExaminationTerm)
                                                             .FirstOrDefault();

            if (prescriptionToDelete == null)
            {
                throw new EntityNotFoundException("Prescription", data);
            }

            if (prescriptionToDelete.Report.Examination.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("You cannot end another prescriber's prescription");
            }

            if (prescriptionToDelete.EndDate <= DateTime.Now)
            {
                throw new ConflictException("The prescription has already ended");
            }

            prescriptionToDelete.EndDate = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
