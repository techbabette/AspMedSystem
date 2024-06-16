using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Users;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Users
{
    public class EfUserDeleteCommand : EfUseCase, IUserDeleteCommand
    {
        private EfUserDeleteCommand()
        {
        }

        public EfUserDeleteCommand(MedSystemContext context) : base(context)
        {
        }

        public string Name => "delete user";

        public void Execute(int data)
        {
            var userToDelete = Context.Users.Where(user => user.Id == data)
                                            .Include(user => user.UserPermissions)
                                            .Include(user => user.UserTreatments)
                                            .Include(user => user.Examinations)
                                            .Include(user => user.ExaminationTerms)
                                            .FirstOrDefault();

            if (userToDelete == null)
            {
                throw new EntityNotFoundException("User", data);
            }

            if (userToDelete.UserTreatments.Any())
            {
                throw new ConflictException("Cannot delete user that has treatments");
            }

            if (userToDelete.Examinations.Any())
            {
                throw new ConflictException("Cannot delete user that has examinations");
            }

            if (userToDelete.ExaminationTerms.Any())
            {
                throw new ConflictException("Cannot delete user that has examination terms");
            }

            Context.UserPermissions.RemoveRange(userToDelete.UserPermissions);

            Context.Users.Remove(userToDelete);
            Context.SaveChanges();
        }
    }
}
