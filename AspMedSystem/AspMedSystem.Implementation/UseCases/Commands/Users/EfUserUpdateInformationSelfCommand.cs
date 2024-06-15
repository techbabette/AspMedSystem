using AspMedSystem.Application;
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
    public class EfUserUpdateInformationSelfCommand : EfUseCase, IUserUpdateInformationSelfCommand
    {
        private IApplicationActor _actor;

        private UserUpdateInformationValidator _validator;
        private EfUserUpdateInformationSelfCommand()
        {
        }

        public EfUserUpdateInformationSelfCommand(MedSystemContext context, IApplicationActor actor, UserUpdateInformationValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public string Name => "Update your own information";

        public void Execute(UserUpdateInformationDTO data)
        {
            data.Id = _actor.Id;
            _validator.ValidateAndThrow(data);
            var userToUpdate = Context.Users.Where(user => user.Id == data.Id)
                                            .FirstOrDefault();

            if(userToUpdate == null)
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
