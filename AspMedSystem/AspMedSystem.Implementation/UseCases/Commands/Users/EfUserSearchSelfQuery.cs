using AspMedSystem.Application.DTO;
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
    public class EfUserSearchSelfQuery : EfUseCase, IUserSearchSingleQuery
    {
        private EfUserSearchSelfQuery()
        {
        }

        public EfUserSearchSelfQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show information about user";

        public UserSearchSingleDTO Execute(int search)
        {
            var user = Context.Users.Where(user => user.Id == search)
                                    .Include(user => user.Group)
                                    .ThenInclude(group => group.GroupPermissions)
                                    .Include(user => user.UserPermissions)
                                    .FirstOrDefault();

            if(user == null)
            {
                throw new EntityNotFoundException("User", search);
            }

            var dto = new UserSearchSingleDTO()
            {
                Email = user.Email,
                Group = user.Group.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Id = user.Id,
                UserAllowedUseCases = user.UserPermissions.Where(permission => permission.Effect == Domain.Effect.Allow).Select(x => x.Permission).ToList(),
                UserDisallowedUseCases = user.UserPermissions.Where(permission => permission.Effect == Domain.Effect.Disallow).Select(x => x.Permission).ToList(),
                GroupAllowedUseCases = user.Group.GroupPermissions.Where(permission => permission.Effect == Domain.Effect.Allow).Select(x => x.Permission).ToList(),
            };

            return dto;
        }
    }
}
