using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.ExaminationTerms;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.ExaminationTerms
{
    public class EfExaminationTermDeleteCommand : EfUseCase, IExaminationTermDeleteCommand
    {
        private EfExaminationTermDeleteCommand()
        {
        }

        public EfExaminationTermDeleteCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Delete examination term";

        public void Execute(int data)
        {
            var examinationTermToDelete = Context.ExaminationTerms.Where(term => term.Id == data)
                                                                  .Include(term => term.Examinations)
                                                                  .FirstOrDefault();

            if (examinationTermToDelete == null)
            {
                throw new EntityNotFoundException("Examination term", data);
            }

            if (examinationTermToDelete.Examinations.Any())
            {
                throw new ConflictException("Cannot delete term that someone already scheduled for (Has examination)");
            }

            Context.ExaminationTerms.Remove(examinationTermToDelete);
            Context.SaveChanges();
        }
    }
}
