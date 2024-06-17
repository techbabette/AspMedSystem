using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Examinations;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Examinations
{
    public class EfExaminationSearchSingleOthersQuery : EfUseCase, IExaminationSearchSingleOthersQuery
    {
        private EfExaminationSearchSingleOthersQuery()
        {
        }

        public EfExaminationSearchSingleOthersQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show any examination";

        public ExaminationTermSearchSingleResultDTO Execute(int search)
        {
            var examination = Context.Examinations.Where(examination => examination.Id == search)
                              .Select(examination => new ExaminationTermSearchSingleResultDTO
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
                                  Reports = examination.Reports.Select(report => report.Id),
                              }).FirstOrDefault();
            
            if (examination == null)
            {
                throw new EntityNotFoundException("Examination", search);
            }

            return examination;
        }
    }
}
