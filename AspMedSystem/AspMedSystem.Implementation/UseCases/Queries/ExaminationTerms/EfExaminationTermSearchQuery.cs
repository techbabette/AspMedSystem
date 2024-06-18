using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.ExaminationTerms;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.ExaminationTerms
{
    public class EfExaminationTermSearchQuery : EfUseCase, IExaminationTermSearchQuery
    {
        private EfExaminationTermSearchQuery()
        {
        }

        public EfExaminationTermSearchQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Search examination terms";

        public PagedResponse<ExaminationTermSearchResultDTO> Execute(ExaminationTermSearchDTO search)
        {
            var query = Context.ExaminationTerms.AsQueryable();
            if (!search.Keyword.IsNullOrEmpty())
            {
                query = query.Where(term =>
                (term.Examiner.FirstName + " " + term.Examiner.LastName).ToLower().Contains(search.Keyword.ToLower()) ||
                term.Examiner.Email.ToLower().Contains(search.Keyword.ToLower())
                );
            }

            if (search.ExaminerId.HasValue)
            {
                query = query.Where(term => term.ExaminerId ==  search.ExaminerId.Value);
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(term => term.Date >= search.DateFrom.Value);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(term => term.Date <= search.DateTo.Value);
            }

            if (search.Available.HasValue)
            {
                if (search.Available.Value)
                {
                    query = query.Where(term => !term.Examinations.Any() && term.Date > DateTime.Now);
                }
                else
                {
                    query = query.Where(term => term.Examinations.Any() || term.Date < DateTime.Now);
                }
            }

            query.OrderByDescending(term => term.Date);
            return query.AsPagedResponse(search, term => new ExaminationTermSearchResultDTO
            {
                TermDate = term.Date,
                ExaminerName = term.Examiner.FirstName + " " + term.Examiner.LastName,
                ExaminerEmail = term.Examiner.Email,
                Id = term.Id,
                ExaminerId = term.ExaminerId,
                Available = !term.Examinations.Any() && term.Date > DateTime.Now
            });
        }
    }
}
