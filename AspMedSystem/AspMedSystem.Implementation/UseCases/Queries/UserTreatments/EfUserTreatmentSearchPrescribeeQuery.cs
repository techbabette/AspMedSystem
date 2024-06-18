using AspMedSystem.Application;
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
    public class EfUserTreatmentSearchPrescribeeQuery : EfUseCase, IUserTreatmentSearchPrescribeeQuery
    {
        private readonly IApplicationActor actor;

        private EfUserTreatmentSearchPrescribeeQuery()
        {
        }

        public EfUserTreatmentSearchPrescribeeQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Search prescriptions as prescribee";

        public PagedResponse<UserTreatmentSearchResultDTO> Execute(UserTreatmentSearchDTO search)
        {
            var query = Context.UserTreatments.AsQueryable();

            query = query.Where(ut => ut.Report.Examination.ExamineeId == actor.Id);

            if (search.PresciberId.HasValue)
            {
                query = query.Where(ut => ut.Report.Examination.ExaminationTerm.ExaminerId == search.PresciberId.Value);
            }

            if (!search.PrescriberKeyword.IsNullOrEmpty())
            {
                query = query.Where(ut =>
                (ut.Report.Examination.ExaminationTerm.Examiner.FirstName + " " + ut.Report.Examination.ExaminationTerm.Examiner.LastName).ToLower().Contains(search.PrescriberKeyword.ToLower()) ||
                ut.Report.Examination.ExaminationTerm.Examiner.Email.ToLower().Contains(search.PrescriberKeyword.ToLower())
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

            query = query.OrderByDescending(ut => ut.StartDate);
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
