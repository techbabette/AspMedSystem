using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Users
{
    public class EfUserSearchSelfPermissionsQuery : EfUseCase, IUserSearchSelfPermissionsQuery
    {
        IApplicationActor _actor;
        private EfUserSearchSelfPermissionsQuery()
        {
        }

        public EfUserSearchSelfPermissionsQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public string Name => "Show your own permissions";

        public UserSearchSinglePermissionsDTO Execute(bool search)
        {
            var user = Context.Users.Where(user => user.Id == _actor.Id)
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
                throw new EntityNotFoundException("User", _actor.Id);
            }

            return user;
        }
    }
}
