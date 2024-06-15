using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases;
using AspMedSystem.Application.UseCases.Queries.Groups;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Groups
{
    public class EfGroupSearchQuery : EfUseCase, IGroupSearchQuery
    {
        public string Name => "Search groups";

        public EfGroupSearchQuery(MedSystemContext context) : base (context)
        {
            
        }

        private EfGroupSearchQuery()
        {

        }

        public PagedResponse<GroupSearchResultDTO> Execute(GroupSearchDTO search)
        {
            var query = Context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(group => group.Name.Contains(search.Name));
            }

            if (search.DefaultRegister.HasValue)
            {
                query = query.Where(group => group.DefaultRegister.Equals(search.DefaultRegister.Value));
            }

            return query.AsPagedResponse(search, group => new GroupSearchResultDTO
            {
                Id = group.Id,
                Name = group.Name,
                NumberOfUsers = group.Users.Count,
                NumberOfPermissionSets = group.GroupPermissions.Count,
                CreatedAt = group.CreatedAt,
                DefaultRegister = group.DefaultRegister
            });
        }
    }
}
