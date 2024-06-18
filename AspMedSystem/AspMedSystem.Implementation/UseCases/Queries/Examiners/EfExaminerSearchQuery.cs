using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.Examiners;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using AspMedSystem.Implementation.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Examiners
{
    public class EfExaminerSearchQuery : EfUseCase, IExaminerSearchQuery
    {
        private EfExaminerSearchQuery()
        {
        }

        public EfExaminerSearchQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Search examiners";
        private static string performExaminationPerm = UseCaseInfo.performExaminationPerm;
        public PagedResponse<ExaminerSearchResultDTO> Execute(ExaminerSearchDTO search)
        {
            var query = Context.Users.AsQueryable();

            query = query.Where(user =>
            ((
                    user.UserPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Allow)
            ||
                    user.Group.GroupPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Allow)
            )
            && !user.UserPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Disallow
            )));

            if (!search.Keyword.IsNullOrEmpty())
            {
                query = query.Where(examiner =>
                    (examiner.FirstName + " " + examiner.LastName).ToLower().Contains(search.Keyword.ToLower()) ||
                    examiner.Email.ToLower().Contains(search.Keyword.ToLower())
                    );
            }

            if (search.Available.HasValue)
            {
                if(search.Available.Value)
                {
                    query = query.Where(examiner => examiner.ExaminationTerms.Any(term => !term.Examinations.Any() && term.Date > DateTime.Now));
                }
                else
                {
                    query = query.Where(examiner => !examiner.ExaminationTerms.Any(term => !term.Examinations.Any() && term.Date > DateTime.Now));
                }
            }

            return query.AsPagedResponse(search, user => new ExaminerSearchResultDTO
            {
                Id = user.Id,
                AvailableTermsCount = user.ExaminationTerms.Where(term => !term.Examinations.Any() && term.Date > DateTime.Now).Count(),
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email
            });
        }
    }
}
