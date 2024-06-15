using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Groups
{
    public class EfGroupUpdateCommand : EfUseCase, IGroupUpdateCommand
    {
        private EfGroupUpdateCommand()
        {
        }

        public EfGroupUpdateCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Update group";

        public void Execute(GroupUpdateDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
