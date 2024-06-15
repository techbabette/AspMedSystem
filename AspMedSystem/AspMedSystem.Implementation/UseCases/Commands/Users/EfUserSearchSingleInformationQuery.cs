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

namespace AspMedSystem.Implementation.UseCases.Commands.Users
{
    public class EfUserSearchSingleInformationQuery : EfUseCase, IUserSearchSingleInformationQuery
    {
        private EfUserSearchSingleInformationQuery()
        {
        }

        public EfUserSearchSingleInformationQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show information about other user";

        public UserSearchSingleInformationDTO Execute(int search)
        {
            var user = Context.Users.Where(user => user.Id == search)
                                    .Select(user => new UserSearchSingleInformationDTO()
                                    {
                                        Email = user.Email,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        BirthDate = user.BirthDate,
                                        Id = user.Id,
                                        Group = user.Group.Name
                                    })
                                    .FirstOrDefault();

            if(user == null)
            {
                throw new EntityNotFoundException("User", search);
            }

            return user;
        }
    }
}
