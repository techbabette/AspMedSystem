using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.ExaminationTerms;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.ExaminationTerms
{
    internal class EfExaminationTermUpdateCommand : EfUseCase, IExaminationTermUpdateCommand
    {
        private EfExaminationTermUpdateCommand()
        {
        }

        public EfExaminationTermUpdateCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Update examination term";

        public void Execute(ExaminationTermUpdateDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
