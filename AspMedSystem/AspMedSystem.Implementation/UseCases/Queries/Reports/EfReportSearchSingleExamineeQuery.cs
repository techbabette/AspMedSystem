using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Reports;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Reports
{
    public class EfReportSearchSingleExamineeQuery : EfUseCase, IReportSearchSingleExamineeQuery
    {
        private readonly IApplicationActor actor;

        private EfReportSearchSingleExamineeQuery()
        {
        }

        public EfReportSearchSingleExamineeQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Show examination as examinee";

        public ReportSearchResultSingleDTO Execute(int search)
        {
            var reportToShow = Context.Reports.Where(report => report.Id == search && report.Examination.ExamineeId == actor.Id)
                                              .Select(report => new ReportSearchResultSingleDTO
                                              {
                                                  Id = report.Id,
                                                  ExaminationId = report.ExaminationId,
                                                  ExamineeId = report.Examination.ExamineeId,
                                                  ExaminerId = report.Examination.ExaminationTerm.ExaminerId,
                                                  ExamineeEmail = report.Examination.Examinee.Email,
                                                  ExaminerEmail = report.Examination.ExaminationTerm.Examiner.Email,
                                                  ExamineeName = report.Examination.Examinee.FirstName + " " + report.Examination.Examinee.LastName,
                                                  ExaminerName = report.Examination.ExaminationTerm.Examiner.FirstName + " " + report.Examination.ExaminationTerm.Examiner.LastName,
                                                  Text = report.Text,
                                                  WrittenOn = report.CreatedAt
                                              })
                                              .FirstOrDefault();

            if (reportToShow == null)
            {
                throw new EntityNotFoundException("Report", search);
            }

            return reportToShow;
        }
    }
}
