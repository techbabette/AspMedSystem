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
    public class EfUserSearchSelfInformationQuery : EfUseCase, IUserSearchSelfInformationQuery
    {
        IApplicationActor _actor;
        private EfUserSearchSelfInformationQuery()
        {
        }

        public EfUserSearchSelfInformationQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public string Name => "Show your own information";

        public UserSearchSingleInformationDTO Execute(bool search)
        {
            var user = Context.Users.Where(user => user.Id == _actor.Id)
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

            if (user == null)
            {
                throw new EntityNotFoundException("User", _actor.Id);
            }

            return user;
        }
    }
}
