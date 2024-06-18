using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.UserTreatments;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.UserTreatments
{
    public class EfUserTreatmentSearchSinglePrescribeeQuery : EfUseCase, IUserTreatmentSearchSinglePrescribeeQuery
    {
        private readonly IApplicationActor actor;

        private EfUserTreatmentSearchSinglePrescribeeQuery()
        {
        }

        public EfUserTreatmentSearchSinglePrescribeeQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Show prescription as prescribee";

        public UserTreatmentSearchSingleResult Execute(int search)
        {
            var prescription = Context.UserTreatments.Where(ut => ut.Id == search && ut.Report.Examination.ExamineeId == actor.Id)
                                                     .Select(userTreatment => new UserTreatmentSearchSingleResult
                                                     {
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
                                                         PrescriberName = userTreatment.Report.Examination.ExaminationTerm.Examiner.FirstName + " " + userTreatment.Report.Examination.ExaminationTerm.Examiner.LastName,
                                                         Note = userTreatment.Note
                                                     })
                                                     .FirstOrDefault();

            if (prescription == null)
            {
                throw new EntityNotFoundException("Prescription", search);
            }

            return prescription;
        }
    }
}
