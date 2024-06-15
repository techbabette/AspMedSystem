using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Groups
{
    public class EfGroupDeleteCommand : EfUseCase, IGroupDeleteCommand
    {
        private EfGroupDeleteCommand()
        {
        }

        public EfGroupDeleteCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Delete Group";

        public void Execute(int data)
        {
            var groupToDelete = Context.Groups.Where(group => group.Id == data)
                                              .Include(group => group.GroupPermissions)
                                              .Include(group => group.Users)
                                              .FirstOrDefault();

            if(groupToDelete == null)
            {
                throw new EntityNotFoundException("Group", data);
            }

            if (groupToDelete.Users.Any())
            {
                throw new ConflictException("Cannot delete group that has users");
            }

            Context.GroupPermissions.RemoveRange(groupToDelete.GroupPermissions);
            Context.Groups.Remove(groupToDelete);

            Context.SaveChanges();
        }
    }
}
