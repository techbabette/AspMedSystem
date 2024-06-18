using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Reports;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Reports
{
    public class EfReportSearchSingleQuery : EfUseCase, IReportSearchSingleQuery
    {
        private EfReportSearchSingleQuery()
        {
        }

        public EfReportSearchSingleQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show report";

        public ReportSearchResultSingleDTO Execute(int search)
        {
            var reportToShow = Context.Reports.Where(report => report.Id == search)
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
                                                  NumberOfPrescriptions = report.UserTreatments.Count,
                                                  PrescriptionIds = report.UserTreatments.Select(tr => tr.Id),
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
