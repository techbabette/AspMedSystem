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
    public class EfExaminationSearchExamineeQuery : EfUseCase, IExaminationSearchExamineeQuery
    {
        private readonly IApplicationActor actor;

        private EfExaminationSearchExamineeQuery()
        {
        }

        public EfExaminationSearchExamineeQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Search examinations as examinee";

        public PagedResponse<ExaminationSearchResultDTO> Execute(ExaminationSearchDTO search)
        {
            var query = Context.Examinations.AsQueryable();

            query = query.Where(examination => examination.ExamineeId == actor.Id);

            if (search.Performed.HasValue)
            {
                query = query.Where(examination => examination.Perfomed.Equals(search.Performed.Value));
            }

            if (search.ExaminerId.HasValue)
            {
                query = query.Where(examination => examination.ExaminationTerm.ExaminerId == search.ExaminerId.Value);
            }

            if (!search.ExaminerKeyword.IsNullOrEmpty())
            {
                query = query.Where(examination =>
                (examination.ExaminationTerm.Examiner.FirstName + " " + examination.ExaminationTerm.Examiner.LastName).ToLower().Contains(search.ExaminerKeyword.ToLower()) ||
                examination.ExaminationTerm.Examiner.Email.ToLower().Contains(search.ExaminerKeyword.ToLower())
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
