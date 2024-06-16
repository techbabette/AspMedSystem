using AspMedSystem.Application;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Reports;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Reports
{
    public class EfReportDeleteCommand : EfUseCase, IReportDeleteCommand
    {
        private readonly IApplicationActor actor;

        private EfReportDeleteCommand()
        {
        }

        public EfReportDeleteCommand(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Delete report";

        public void Execute(int data)
        {
            var reportToDelete = Context.Reports.Where(report => report.Id == data)
                                                .Include(report => report.Examination)
                                                .ThenInclude(examination => examination.ExaminationTerm)
                                                .Include(report => report.UserTreatments)
                                                .FirstOrDefault();

            if(reportToDelete == null)
            {
                throw new EntityNotFoundException("Report", data);
            }

            if(reportToDelete.Examination.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("Cannot delete someone else's report");
            }

            if (reportToDelete.UserTreatments.Any())
            {
                throw new ConflictException("Cannot delete a report used to prescribe user treatments");
            }

            Context.Reports.Remove(reportToDelete);
            Context.SaveChanges();
        }
    }
}
