using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.Examinations;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Examinations
{
    public class EfExaminationSearchExaminerQuery : EfUseCase, IExaminationSearchExaminerQuery
    {
        private readonly IApplicationActor actor;

        private EfExaminationSearchExaminerQuery()
        {
        }

        public EfExaminationSearchExaminerQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Search examinations as examiner";

        public PagedResponse<ExaminationSearchResultDTO> Execute(ExaminationSearchDTO search)
        {
            var query = Context.Examinations.AsQueryable();

            query = query.Where(examination => examination.ExaminationTerm.ExaminerId == actor.Id);

            if (search.Performed.HasValue)
            {
                query = query.Where(examination => examination.Perfomed.Equals(search.Performed.Value));
            }

            if (search.ExamineeId.HasValue)
            {
                query = query.Where(examination => examination.ExamineeId == search.ExamineeId.Value);
            }

            if (!search.ExamineeKeyword.IsNullOrEmpty())
            {
                query = query.Where(examination =>
                (examination.Examinee.FirstName + " " + examination.Examinee.LastName).ToLower().Contains(search.ExamineeKeyword.ToLower()) ||
                examination.Examinee.Email.ToLower().Contains(search.ExamineeKeyword.ToLower())
                );
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(examination => examination.ExaminationTerm.Date >= search.DateFrom.Value);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(examination => examination.ExaminationTerm.Date <= search.DateTo.Value);
            }

            return query.AsPagedResponse(search, examination => new ExaminationSearchResultDTO
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
                NumberOfReports = examination.Reports.Count
            });
        }
    }
}
