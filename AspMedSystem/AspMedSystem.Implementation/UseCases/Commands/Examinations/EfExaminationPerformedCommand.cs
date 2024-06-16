using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Examinations;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Examinations
{
    public class EfExaminationPerformedCommand : EfUseCase, IExaminationPerformedCommand
    {
        private readonly IApplicationActor actor;

        private EfExaminationPerformedCommand()
        {
        }

        public EfExaminationPerformedCommand(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "mark examination as performed";

        public void Execute(int data)
        {
            var examinationToMark = Context.Examinations.Where(examination => examination.Id == data)
                                                        .Include(examination => examination.ExaminationTerm)
                                                        .FirstOrDefault();

            if(examinationToMark == null)
            {
                throw new EntityNotFoundException("Examination", data);
            }

            if(examinationToMark.ExaminationTerm.ExaminerId != actor.Id)
            {
                throw new ConflictException("Examination does not belong to you");
            }

            examinationToMark.Perfomed = true;
            Context.SaveChanges();
        }
    }
}
