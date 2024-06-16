using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Users;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Users
{
    public class EfUserUpdatePermissionsCommand : EfUseCase, IUserUpdatePermissionsCommand
    {
        private readonly UserUpdatePermissionsValidator _validator;

        private EfUserUpdatePermissionsCommand()
        {
        }

        public EfUserUpdatePermissionsCommand(MedSystemContext context, UserUpdatePermissionsValidator validator) : base(context)
        {
            _validator = validator;
        }

        public string Name => "Update user permissions";

        public void Execute(UserUpdatePermissionsDTO data)
        {
            _validator.ValidateAndThrow(data);

            var userToUpdate = Context.Users.Where(user => user.Id == data.Id)
                                            .Include(user => user.UserPermissions)
                                            .FirstOrDefault();
            
            if(userToUpdate == null)
            {
                throw new EntityNotFoundException("User", data.Id);
            }

            var newPermissions = new List<UserPermission>();

            newPermissions.AddRange(data.AllowedUseCases.Select(useCase => new UserPermission
            {
                Permission = useCase,
                Effect = Effect.Allow
            }));

            newPermissions.AddRange(data.DisallowedUseCases.Select(useCase => new UserPermission
            {
                Permission = useCase,
                Effect = Effect.Disallow
            }));

            Context.UserPermissions.RemoveRange(userToUpdate.UserPermissions);

            userToUpdate.UserPermissions = newPermissions;

            Context.SaveChanges();
        }
    }
}
