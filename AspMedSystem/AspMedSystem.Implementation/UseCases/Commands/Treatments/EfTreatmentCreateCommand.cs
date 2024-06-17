using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Treatments
{
    public class EfTreatmentCreateCommand : EfUseCase, ITreatmentCreateCommand
    {
        public string Name => "Create treatment";

        public void Execute(TreatmentCreateDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
