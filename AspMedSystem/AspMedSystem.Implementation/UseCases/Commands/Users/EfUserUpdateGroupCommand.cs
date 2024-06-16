using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Users;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Users
{
    public class EfUserUpdateGroupCommand : EfUseCase, IUserUpdateGroupCommand
    {
        private EfUserUpdateGroupCommand()
        {
        }

        public EfUserUpdateGroupCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Update user group";

        public void Execute(UserUpdateGroupDTO data)
        {
            var userToUpdate = Context.Users.Where(user => user.Id == data.Id)
                                            .FirstOrDefault();

            if (userToUpdate == null)
            {
                throw new EntityNotFoundException("User", data.Id);
            }

            var newGroup = Context.Groups.Find(data.GroupId);

            if (newGroup == null)
            {
                throw new EntityNotFoundException("Group", data.Id);
            }

            userToUpdate.Group = newGroup;
            Context.SaveChanges();
        }
    }
}
