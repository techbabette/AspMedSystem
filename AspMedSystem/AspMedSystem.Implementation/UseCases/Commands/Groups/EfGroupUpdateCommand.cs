using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Commands.Groups;
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

namespace AspMedSystem.Implementation.UseCases.Commands.Groups
{
    public class EfGroupUpdateCommand : EfUseCase, IGroupUpdateCommand
    {
        private GroupUpdateValidator _validator;
        private EfGroupUpdateCommand()
        {
        }

        public EfGroupUpdateCommand(MedSystemContext context, GroupUpdateValidator validator) : base(context)
        {
            _validator = validator;
        }

        public string Name => "Update group";

        public void Execute(GroupUpdateDTO data)
        {
            _validator.ValidateAndThrow(data);

            var groupToUpdate = Context.Groups.Where(group => group.Id == data.Id)
                                               .Include(group => group.GroupPermissions)
                                               .FirstOrDefault();

            if(groupToUpdate == null)
            {
                throw new EntityNotFoundException("Group", data.Id);
            }

            var newPermissions = new List<GroupPermission>();

            newPermissions.AddRange(data.AllowedUseCases.Select(useCase => new GroupPermission
            {
                Permission = useCase,
                Effect = Effect.Allow
            }));

            Context.GroupPermissions.RemoveRange(groupToUpdate.GroupPermissions);

            groupToUpdate.Name = data.Name;
            groupToUpdate.DefaultRegister = data.DefaultRegister;
            groupToUpdate.GroupPermissions = newPermissions;

            Context.SaveChanges();
        }
    }
}
