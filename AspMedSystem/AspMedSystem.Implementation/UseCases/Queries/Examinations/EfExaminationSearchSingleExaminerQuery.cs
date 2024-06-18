using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Examinations;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Examinations
{
    public class EfExaminationSearchSingleExaminerQuery : EfUseCase, IExaminationSearchSingleExaminerQuery
    {
        private readonly IApplicationActor actor;

        private EfExaminationSearchSingleExaminerQuery()
        {
        }

        public EfExaminationSearchSingleExaminerQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Show your examinations as examiner";

        public ExaminationSearchSingleResultDTO Execute(int search)
        {
            var examination = Context.Examinations.Where(examination => examination.Id == search)
                              .Select(examination => new ExaminationSearchSingleResultDTO
                              {
                                  Id = examination.Id,
                                  Performed = examination.Perfomed,
                                  ExamineeId = examination.ExamineeId,
                                  ExaminerId = examination.ExaminationTerm.ExaminerId,
                                  Date = examination.ExaminationTerm.Date,
                                  ExamineeEmail = examination.Examinee.Email,
                                  ExaminerEmail = examination.ExaminationTerm.Examiner.Email,
                                  ExamineeName = examination.Examinee.FirstName + " " + examination.Examinee.LastName,
                                  ExaminerName = examination.ExaminationTerm.Examiner.FirstName + " " + examination.ExaminationTerm.Examiner.LastName,
                                  NumberOfReports = examination.Reports.Count,
                                  ReportIds = examination.Reports.Select(report => report.Id),
                              }).FirstOrDefault();

            if (examination == null)
            {
                throw new EntityNotFoundException("Examination", search);
            }

            if (examination.ExaminerId != actor.Id)
            {
                throw new ConflictException("This examination does not belong to you");
            }

            return examination;
        }
    }
}
