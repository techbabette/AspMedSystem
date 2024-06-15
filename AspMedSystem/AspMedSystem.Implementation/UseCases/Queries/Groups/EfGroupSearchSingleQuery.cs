using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Groups;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Groups
{
    public class EfGroupSearchSingleQuery : EfUseCase, IGroupSearchSingleQuery
    {
        public string Name => "Search single group";

        public EfGroupSearchSingleQuery(MedSystemContext context) : base(context)
        {
            
        }
        private EfGroupSearchSingleQuery()
        {
            
        }

        public GroupSearchSingleResultDTO Execute(int search)
        {
            var group = Context.Groups.Where(group => group.Id == search)
                                      .Select(group => new GroupSearchSingleResultDTO
            {
                Name = group.Name,
                Users = group.Users.Select(user => user.Email),
                CreatedAt = group.CreatedAt,
                DefaultRegister = group.DefaultRegister,
                AllowedUseCases = group.GroupPermissions.Where(groupPermission => groupPermission.Effect == Domain.Effect.Allow).Select(groupPermission => groupPermission.Permission),
                DisallowedUseCases = group.GroupPermissions.Where(groupPermission => groupPermission.Effect == Domain.Effect.Disallow).Select(groupPermission => groupPermission.Permission),
            }).FirstOrDefault();

            if(group == null)
            {
                throw new EntityNotFoundException("Group", search);
            }

            return group;
        }
    }
}
