using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.UserTreatments;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.UserTreatments
{
    public class EfUserTreatmentCreateCommand : EfUseCase, IUserTreatmentCreateCommand
    {
        private readonly IApplicationActor actor;
        private readonly UserTreatmentCreateValidator validator;

        private EfUserTreatmentCreateCommand()
        {
        }

        public EfUserTreatmentCreateCommand(MedSystemContext context, IApplicationActor actor, UserTreatmentCreateValidator validator) : base(context)
        {
            this.actor = actor;
            this.validator = validator;
        }

        public string Name => "Prescribe user treatment";

        public void Execute(UserTreatmentCreateDTO data)
        {
            validator.ValidateAndThrow(data);
            var reportToBaseTreatmentOn = Context.Reports.Where(report => report.Id == data.ReportId)
                                                         .Include(report => report.Examination)
                                                         .ThenInclude(examination => examination.ExaminationTerm)
                                                         .FirstOrDefault();

            if (reportToBaseTreatmentOn == null)
            {
                throw new SubEntityNotFoundException("ReportId", data.ReportId);
            }

            if (reportToBaseTreatmentOn.Examination.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("Cannot base prescription on someone else's report");
            }

            /*var userToPrescribeTo = Context.Users.Where(user => user.Id == reportToBaseTreatmentOn.Examination.ExamineeId)
                                                 .Include(user => user.UserTreatments)
                                                 .FirstOrDefault();*/

            var userHasTreatmentAlready = Context.UserTreatments.Where(ut => ut.UserId == reportToBaseTreatmentOn.Examination.ExamineeId && ut.TreatmentId == data.TreatmentId
            && (
            (
            (data.StartDate < ut.EndDate && data.StartDate >= ut.StartDate) ||
            (data.EndDate <= ut.EndDate && data.EndDate > ut.StartDate) ||
            (data.StartDate <= ut.StartDate && data.EndDate >= ut.EndDate)
            ))).Any();

            /*var userHasTreatmentActive = userToPrescribeTo.UserTreatments.Any(treatment => treatment.TreatmentId == data.TreatmentId &&
            (
            !treatment.EndDate.HasValue || treatment.EndDate > data.StartDate
            ));*/

            if (userHasTreatmentAlready)
            {
                throw new ConflictException("Cannot prescribe a treatment the user already has active, try ending the existing prescription first or adjusting the start and end dates");
            }

            var treatmentToPrescribe = Context.Treatments.Find(data.TreatmentId);

            if (treatmentToPrescribe == null)
            {
                throw new SubEntityNotFoundException("TreatmentId", data.TreatmentId);
            }

            if (!treatmentToPrescribe.Prescribable)
            {
                throw new ConflictException("The treatment is not prescribable");
            }

            Context.UserTreatments.Add(new UserTreatment
            {
                ReportId = data.ReportId,
                UserId = reportToBaseTreatmentOn.Examination.ExamineeId,
                Treatment = treatmentToPrescribe,
                Note = data.Note.IsNullOrEmpty() ? null : data.Note,
                StartDate = data.StartDate,
                EndDate = data.EndDate
            });
            Context.SaveChanges();
        }
    }
}
