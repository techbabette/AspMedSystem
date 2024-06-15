using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Auth;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Auth
{
    public class EfAuthRegisterCommand : EfUseCase, IAuthRegisterCommand
    {
        public string Name => "Register";

        private AuthRegisterValidator _validator;

        public EfAuthRegisterCommand(MedSystemContext context, AuthRegisterValidator validator) : base(context)
        {
            _validator = validator;
        }
        private EfAuthRegisterCommand() { }
        public void Execute(AuthRegisterDTO data)
        {
            _validator.ValidateAndThrow(data);

            User newUser = new User
            {
                BirthDate = data.BirthDate.Value,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
            };

            Group DefaultNewUserGroup = Context.Groups.FirstOrDefault(group => group.DefaultRegister);

            newUser.Group = DefaultNewUserGroup;

            Context.Users.Add(newUser);
            Context.SaveChanges();
        }
    }
}
