using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Users;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Users
{
    public class EfUserUpdateInformationOthersCommand : EfUseCase, IUserUpdateInformationOthersCommand
    {
        private UserUpdateInformationValidator _validator;
        private EfUserUpdateInformationOthersCommand()
        {
        }

        public EfUserUpdateInformationOthersCommand(MedSystemContext context, UserUpdateInformationValidator validator) : base(context)
        {
            _validator = validator;
        }

        public string Name => "Updated others' information";

        public void Execute(UserUpdateInformationDTO data)
        {
            _validator.ValidateAndThrow(data);
            var userToUpdate = Context.Users.Where(user => user.Id == data.Id)
                                .FirstOrDefault();

            if (userToUpdate == null)
            {
                throw new EntityNotFoundException("User", data.Id);
            }

            userToUpdate.FirstName = data.FirstName;
            userToUpdate.LastName = data.LastName;
            userToUpdate.Email = data.Email;

            Context.SaveChanges();
        }
    }
}
