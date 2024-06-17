using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.DataAccess;
using AutoMapper;
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
        private readonly IMapper mapper;

        private EfUserSearchSelfInformationQuery()
        {
        }

        public EfUserSearchSelfInformationQuery(MedSystemContext context, IApplicationActor actor, IMapper mapper) : base(context)
        {
            _actor = actor;
            this.mapper = mapper;
        }

        public string Name => "Show your own information";

        public UserSearchSingleInformationDTO Execute(bool search)
        {
            var query = Context.Users.Where(user => user.Id == _actor.Id);

            var user = mapper.ProjectTo<UserSearchSingleInformationDTO>(query).FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException("User", _actor.Id);
            }

            return user;
        }
    }
}
