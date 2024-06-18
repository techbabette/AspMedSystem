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
    public class EfUserTreatmentUpdateCommand : EfUseCase, IUserTreatmentUpdateCommand
    {
        private readonly IApplicationActor actor;
        private readonly UserTreatmentUpdateValidator validator;

        private EfUserTreatmentUpdateCommand()
        {
        }

        public EfUserTreatmentUpdateCommand(MedSystemContext context, IApplicationActor actor, UserTreatmentUpdateValidator validator) : base(context)
        {
            this.actor = actor;
            this.validator = validator;
        }

        public string Name => "Update prescription";

        public void Execute(UserTreatmentUpdateDTO data)
        {
            validator.ValidateAndThrow(data);

            var prescriptionToUpdate = Context.UserTreatments.Where(tr => tr.Id == data.Id)
                                                             .Include(tr => tr.Report)
                                                             .ThenInclude(report => report.Examination)
                                                             .ThenInclude(exam => exam.ExaminationTerm)
                                                             .FirstOrDefault();

            if (prescriptionToUpdate == null)
            {
                throw new EntityNotFoundException("Prescription", data.Id);
            }

            if (prescriptionToUpdate.Report.Examination.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("You cannot edit another prescriber's prescription");
            }

            if (prescriptionToUpdate.StartDate < DateTime.Now)
            {
                throw new ConflictException("Cannot edit a started prescription, try stopping it and starting a new one");
            }

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

            var userHasTreatmentAlready = Context.UserTreatments.Where(ut => ut.UserId == reportToBaseTreatmentOn.Examination.ExamineeId && ut.TreatmentId == data.TreatmentId
            &&  ut.Id != data.Id && (
            (
            (data.StartDate < ut.EndDate && data.StartDate >= ut.StartDate) ||
            (data.EndDate <= ut.EndDate && data.EndDate > ut.StartDate) ||
            (data.StartDate <= ut.StartDate && data.EndDate >= ut.EndDate && ut.EndDate >= ut.StartDate)
            ))).Any();

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

            prescriptionToUpdate.Note = data.Note;
            prescriptionToUpdate.StartDate = data.StartDate;
            prescriptionToUpdate.EndDate = data.EndDate;
            prescriptionToUpdate.UserId = reportToBaseTreatmentOn.Examination.ExamineeId;
            prescriptionToUpdate.Treatment = treatmentToPrescribe;

            Context.SaveChanges();
        }
    }
}
