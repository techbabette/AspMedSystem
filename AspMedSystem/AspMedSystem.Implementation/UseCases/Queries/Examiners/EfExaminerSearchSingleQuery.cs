using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Examiners;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Examiners
{
    public class EfExaminerSearchSingleQuery : EfUseCase, IExaminerSearchSingleQuery
    {
        private EfExaminerSearchSingleQuery()
        {
        }

        public EfExaminerSearchSingleQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show examiner";
        private static string performExaminationPerm = "mark examination as performed";
        public ExaminerSearchResultSingleDTO Execute(int search)
        {
            var examiner = Context.Users.Where(user => user.Id == search &&
                    (
                    user.UserPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Allow)
                    ||
                    user.Group.GroupPermissions.Any(perm => perm.Permission.Equals(performExaminationPerm) && perm.Effect == Domain.Effect.Allow)
                    ))
                    .Select(user => new ExaminerSearchResultSingleDTO
                    {
                        Id = user.Id,
                        Name = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        AvailableTermsCount = user.ExaminationTerms.Where(term => !term.Examinations.Any() && term.Date > DateTime.Now).Count(),
                        AvailableTerms = user.ExaminationTerms.Where(term => !term.Examinations.Any() && term.Date > DateTime.Now).Select(term => term.Id)
                    })
                    .FirstOrDefault();

            if (examiner == null)
            {
                throw new EntityNotFoundException("Examiner", search);
            }

            return examiner;
        }
    }
}
