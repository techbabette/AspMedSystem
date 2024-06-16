using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.ExaminationTerms;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.ExaminationTerms
{
    public class EfExaminationTermUpdateCommand : EfUseCase, IExaminationTermUpdateCommand
    {
        private readonly ExaminationTermUpdateValidator validator;

        private EfExaminationTermUpdateCommand()
        {
        }

        public EfExaminationTermUpdateCommand(MedSystemContext context, ExaminationTermUpdateValidator validator) : base(context)
        {
            this.validator = validator;
        }

        public string Name => "Update examination term";

        public void Execute(ExaminationTermUpdateDTO data)
        {
            validator.ValidateAndThrow(data);

            var examinationTermToUpdate = Context.ExaminationTerms.Where(term => term.Id == data.Id)
                                                                  .Include(term => term.Examinations)
                                                                  .FirstOrDefault();

            if(examinationTermToUpdate == null)
            {
                throw new EntityNotFoundException("Examination term", data.Id);
            }

            if(examinationTermToUpdate.Date < DateTime.Now)
            {
                throw new ConflictException("Cannot change information about term that's in the past");
            }

            if(examinationTermToUpdate.Examinations.Any())
            {
                throw new ConflictException("Cannot change information about term that someone already scheduled for (Has examination)");
            }

            examinationTermToUpdate.Date = data.DateTime;
            examinationTermToUpdate.ExaminerId = data.ExaminerId;

            Context.SaveChanges();
        }
    }
}
