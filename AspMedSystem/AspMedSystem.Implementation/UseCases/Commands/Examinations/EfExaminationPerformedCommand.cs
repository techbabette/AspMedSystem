using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Examinations;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Examinations
{
    public class EfExaminationPerformedCommand : EfUseCase, IExaminationPerformedCommand
    {
        private readonly ExaminationPerformedValidator validator;
        private readonly IApplicationActor actor;

        private EfExaminationPerformedCommand()
        {
        }

        public EfExaminationPerformedCommand(MedSystemContext context, ExaminationPerformedValidator validator, IApplicationActor actor) : base(context)
        {
            this.validator = validator;
            this.actor = actor;
        }

        public string Name => "mark examination as performed";

        public void Execute(ExaminationPerformedDTO data)
        {
            data.ExaminerId = actor.Id;
            validator.ValidateAndThrow(data);
            var examinationToMark = Context.Examinations.Find(data.ExaminationId);

            if(examinationToMark == null)
            {
                throw new EntityNotFoundException("Examination", data.ExaminationId);
            }

            examinationToMark.Perfomed = true;

            Context.SaveChanges();
        }
    }
}
