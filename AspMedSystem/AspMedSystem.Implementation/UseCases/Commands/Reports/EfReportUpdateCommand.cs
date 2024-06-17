using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
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
    public class EfReportUpdateCommand : EfUseCase, IReportUpdateCommand
    {
        private readonly IApplicationActor actor;

        private EfReportUpdateCommand()
        {
        }

        public EfReportUpdateCommand(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Update report";
        public void Execute(ReportUpdateDTO data)
        {
            var reportToUpdate = Context.Reports.Where(report => report.Id == data.Id)
                                                .Include(report => report.UserTreatments)
                                                .Include(report => report.Examination)
                                                .ThenInclude(report => report.ExaminationTerm)
                                                .FirstOrDefault();

            if (reportToUpdate == null)
            {
                throw new EntityNotFoundException("Report", data.Id);
            }

            if (reportToUpdate.Examination.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("Cannot edit someone else's report");
            }

            if (reportToUpdate.UserTreatments.Any())
            {
                throw new ConflictException("Cannot change report used to prescribe user treatments");
            }

            var examinationUsedAsNewBasis = Context.Examinations.Where(examination => examination.Id == data.ExaminationId)
                                                             .Include(examination => examination.ExaminationTerm)
                                                             .FirstOrDefault();

            if (examinationUsedAsNewBasis == null)
            {
                throw new SubEntityNotFoundException("ExaminationId", data.ExaminationId);
            }

            if (examinationUsedAsNewBasis.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("Cannot write a report based on someone else's examination");
            }

            if (!examinationUsedAsNewBasis.Perfomed)
            {
                throw new ConflictException("Cannot write a report based on an unperformed examination");
            }

            reportToUpdate.Examination = examinationUsedAsNewBasis;
            reportToUpdate.Text = data.Text;
            Context.SaveChanges();
        }
    }
}
