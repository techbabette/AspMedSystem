using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Examinations;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Examinations
{
    public class EfExaminationCreateCommand : EfUseCase, IExaminationCreateCommand
    {
        private readonly IApplicationActor actor;

        private EfExaminationCreateCommand()
        {
        }

        public EfExaminationCreateCommand(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Schedule an examination";

        public void Execute(ExaminationCreateDTO data)
        {
            data.ExamineeId = actor.Id;

            var examinationTerm = Context.ExaminationTerms.Where(term => term.Id == data.ExaminationTermId)
                                                           .Include(term => term.Examinations)
                                                           .FirstOrDefault();

            if (examinationTerm == null)
            {
                throw new EntityNotFoundException("Examination term", data.ExaminationTermId);
            }

            if(examinationTerm.Date <  DateTime.Now)
            {
                throw new ConflictException("Cannot schedule for a term that's in the past");
            }

            if (examinationTerm.Examinations.Any())
            {
                throw new ConflictException("Examination term is not available");
            }

            if(examinationTerm.ExaminerId == actor.Id)
            {
                throw new ConflictException("You cannot schedule an examination with yourself");
            }

            Context.Examinations.Add(new Domain.Examination
            {
                ExaminationTermId = data.ExaminationTermId,
                ExamineeId = data.ExamineeId,
                Perfomed = false
            });

            Context.SaveChanges();
        }
    }
}
