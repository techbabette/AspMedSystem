using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.DataAccess.Migrations;
using AspMedSystem.Domain;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.Groups
{
    public class EfGroupCreateCommand : EfUseCase, IGroupCreateCommand
    {
        public string Name => "Create Group";

        private GroupCreateValidator _validator;

        public EfGroupCreateCommand(GroupCreateValidator validator)
        {
            _validator = validator;
        }

        public void Execute(GroupCreateDTO data)
        {
            _validator.ValidateAndThrow(data);

            var allowed = data.AllowedUseCases.Select(useCase => new GroupPermission
            {
                Permission = useCase,
                Effect = Effect.Allow
            });

            var disallowed = data.DisallowedUseCases.Select(useCase => new GroupPermission
            {
                Permission = useCase,
                Effect = Effect.Disallow
            });

            var permissions = new List<GroupPermission>();

            permissions.AddRange(allowed);
            permissions.AddRange(disallowed);


            Group newGroup = new Group
            {
                Name = data.Name,
                DefaultRegister = data.DefaultRegister ?? false,
                GroupPermissions = permissions
            };

            Context.Groups.Add(newGroup);

            Context.SaveChanges();
        }
    }
}
