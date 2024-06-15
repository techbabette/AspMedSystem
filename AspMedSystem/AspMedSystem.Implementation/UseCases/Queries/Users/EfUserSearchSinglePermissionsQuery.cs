using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Users
{
    public class EfUserSearchSinglePermissionsQuery : EfUseCase, IUserSearchSinglePermissionsQuery
    {
        private EfUserSearchSinglePermissionsQuery()
        {
        }

        public EfUserSearchSinglePermissionsQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show other users permissions";

        public UserSearchSinglePermissionsDTO Execute(int search)
        {
            var user = Context.Users.Where(user => user.Id == search)
                                    .Select(user => new UserSearchSinglePermissionsDTO
                                    {
                                        Id = user.Id,
                                        Group = user.Group.Name,
                                        GroupAllowedPermissions = user.Group.GroupPermissions
                                          .Where(permission => permission.Effect == Domain.Effect.Allow)
                                          .Select(permission => permission.Permission),

                                        UserAllowedPermissions = user.UserPermissions
                                         .Where(permission => permission.Effect == Domain.Effect.Allow)
                                         .Select(permission => permission.Permission),

                                        UserDisallowedPermissions = user.UserPermissions
                                            .Where(permission => permission.Effect == Domain.Effect.Disallow)
                                            .Select(permission => permission.Permission),
                                    })
                                    .FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException("User", search);
            }

            return user;
        }
    }
}
