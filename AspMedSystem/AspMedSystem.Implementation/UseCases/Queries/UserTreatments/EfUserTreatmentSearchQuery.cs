using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.UserTreatments;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.UserTreatments
{
    public class EfUserTreatmentSearchQuery : EfUseCase, IUserTreatmentSearchQuery
    {
        private EfUserTreatmentSearchQuery()
        {
        }

        public EfUserTreatmentSearchQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Search user treatments";

        public PagedResponse<UserTreatmentSearchResultDTO> Execute(UserTreatmentSearchDTO search)
        {
            var query = Context.UserTreatments.AsQueryable();

            if (search.PresciberId.HasValue) 
            { 
                query = query.Where(ut => ut.Report.Examination.ExaminationTerm.ExaminerId ==  search.PresciberId.Value);
            }

            if (!search.PrescriberKeyword.IsNullOrEmpty())
            {
                query = query.Where(ut =>
                (ut.Report.Examination.ExaminationTerm.Examiner.FirstName + " " + ut.Report.Examination.ExaminationTerm.Examiner.LastName).ToLower().Contains(search.PrescriberKeyword.ToLower()) ||
                ut.Report.Examination.ExaminationTerm.Examiner.Email.ToLower().Contains(search.PrescriberKeyword.ToLower())
                );
            }

            if (search.PrescribeeId.HasValue)
            {
                query = query.Where(ut => ut.Report.Examination.ExamineeId == search.PrescribeeId.Value);
            }

            if (!search.PrescribeeKeyword.IsNullOrEmpty())
            {
                query = query.Where(ut =>
                (ut.Report.Examination.Examinee.FirstName + " " + ut.Report.Examination.Examinee.LastName).ToLower().Contains(search.PrescribeeKeyword.ToLower()) ||
                ut.Report.Examination.Examinee.Email.ToLower().Contains(search.PrescribeeKeyword.ToLower())
                );
            }

            if (search.TreatmentId.HasValue)
            {
                query = query.Where(ut => ut.TreatmentId == search.TreatmentId.Value);
            }

            if (!search.TreatmentKeyword.IsNullOrEmpty())
            {
                query = query.Where(ut => ut.Treatment.Name.ToLower().Contains(search.TreatmentKeyword.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(ut => ut.StartDate >= search.DateFrom.Value);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(ut => ut.StartDate <= search.DateTo.Value);
            }

            query.OrderByDescending(ut => ut.StartDate);
            return query.AsPagedResponse(search, userTreatment => new UserTreatmentSearchResultDTO
            {
                Id = userTreatment.Id,
                StartDate = userTreatment.StartDate,
                EndDate = userTreatment.EndDate,
                ReportId = userTreatment.ReportId,
                TreatmentId = userTreatment.TreatmentId,
                Treatment = userTreatment.Treatment.Name,
                PrescribeeId = userTreatment.Report.Examination.ExamineeId,
                PrescribeeEmail = userTreatment.Report.Examination.Examinee.Email,
                PrescribeeName = userTreatment.Report.Examination.Examinee.FirstName + " " + userTreatment.Report.Examination.Examinee.LastName,
                PrescriberId = userTreatment.Report.Examination.ExaminationTerm.ExaminerId,
                PrescriberEmail = userTreatment.Report.Examination.ExaminationTerm.Examiner.Email,
                PrescriberName = userTreatment.Report.Examination.ExaminationTerm.Examiner.FirstName + " " + userTreatment.Report.Examination.ExaminationTerm.Examiner.LastName
            });
        }
    }
}
