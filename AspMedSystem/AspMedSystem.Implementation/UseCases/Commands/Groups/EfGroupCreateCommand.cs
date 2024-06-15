using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.DataAccess;
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

        public EfGroupCreateCommand(MedSystemContext context, GroupCreateValidator validator) : base(context)
        {
            _validator = validator;
        }
        private EfGroupCreateCommand() { }

        public void Execute(GroupCreateDTO data)
        {
            _validator.ValidateAndThrow(data);

            var allowed = data.AllowedUseCases.Distinct().Select(useCase => new GroupPermission
            {
                Permission = useCase.ToLower(),
                Effect = Effect.Allow
            });

            var permissions = new List<GroupPermission>();

            permissions.AddRange(allowed);

            Group newGroup = new Group
            {
                Name = data.Name,
                DefaultRegister = data.DefaultRegister.HasValue ? data.DefaultRegister.Value : false,
                GroupPermissions = permissions
            };

            Context.Groups.Add(newGroup);

            Context.SaveChanges();
        }
    }
}
