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
    public class EfExaminationDeleteCommand : EfUseCase, IExaminationDeleteCommand
    {
        private EfExaminationDeleteCommand()
        {
        }

        public EfExaminationDeleteCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Unschedule examination";

        public void Execute(int data)
        {
            var examinationToDelete = Context.Examinations.Where(examination => examination.Id == data)
                                                          .Include(examination => examination.ExaminationTerm)
                                                          .FirstOrDefault();

            if(examinationToDelete == null)
            {
                throw new EntityNotFoundException("Examination", data);
            }

            if (examinationToDelete.Perfomed)
            {
                throw new ConflictException("Cannot unschedule performed examination");
            }

            if (examinationToDelete.ExaminationTerm.Date <= DateTime.Now)
            {
                throw new ConflictException("Cannot unschedule examination after examination date");
            }

            Context.Examinations.Remove(examinationToDelete);
            Context.SaveChanges();
        }
    }
}
