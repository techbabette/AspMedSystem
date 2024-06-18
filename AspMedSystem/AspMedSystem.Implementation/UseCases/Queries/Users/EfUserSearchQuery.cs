using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using AspMedSystem.Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Users
{
    public class EfUserSearchQuery : EfUseCase, IUserSearchQuery
    {
        private EfUserSearchQuery()
        {
        }

        public EfUserSearchQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Search users";

        public PagedResponse<UserSearchResultDTO> Execute(UserSearchDTO search)
        {
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(user => user.Email.ToLower().Contains(search.Keyword.ToLower())
                || user.Group.Name.ToLower().Contains(search.Keyword.ToLower()));
            }

            query = query.OrderByDescending(x => x.CreatedAt);
            return query.AsPagedResponse(search, user => new UserSearchResultDTO
            {
                Id = user.Id,
                Email = user.Email,
                Group = user.Group.Name,
                CreatedAt = user.CreatedAt
            });
        }
    }
}
