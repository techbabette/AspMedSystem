using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Reports;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Reports
{
    public class EfReportCreateCommand : EfUseCase, IReportCreateCommand
    {
        private readonly ReportCreateValidator validator;
        private readonly IApplicationActor actor;

        private EfReportCreateCommand()
        {
        }

        public EfReportCreateCommand(MedSystemContext context, ReportCreateValidator validator, IApplicationActor actor) : base(context)
        {
            this.validator = validator;
            this.actor = actor;
        }

        public string Name => "Create report";

        public void Execute(ReportCreateDTO data)
        {
            validator.ValidateAndThrow(data);

            var examinationUsedAsBasis = Context.Examinations.Where(examination => examination.Id == data.ExaminationId)
                                                             .Include(examination => examination.ExaminationTerm)
                                                             .FirstOrDefault();

            if(examinationUsedAsBasis == null)
            {
                throw new EntityNotFoundException("Examination", data.ExaminationId);
            }

            if(examinationUsedAsBasis.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("Cannot write a report based on someone else's examination");
            }

            if(!examinationUsedAsBasis.Perfomed)
            {
                throw new ConflictException("Cannot write a report based on an unperformed examination");
            }

            Context.Reports.Add(new Domain.Report
            {
                Examination = examinationUsedAsBasis,
                Text = data.Text
            });
            Context.SaveChanges();
        }
    }
}
