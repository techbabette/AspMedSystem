using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Users;
using AspMedSystem.DataAccess;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AspMedSystem.Implementation.UseCases.Queries.Users
{
    public class EfUserSearchSingleInformationQuery : EfUseCase, IUserSearchSingleInformationQuery
    {
        private readonly IMapper mapper;

        private EfUserSearchSingleInformationQuery()
        {
        }

        public EfUserSearchSingleInformationQuery(MedSystemContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public string Name => "Show information about other user";

        public UserSearchSingleInformationDTO Execute(int search)
        {
            var query = Context.Users.Where(user => user.Id == search);

            var user = mapper.ProjectTo<UserSearchSingleInformationDTO>(query).FirstOrDefault();

            if (user == null)
            {
                throw new EntityNotFoundException("User", search);
            }

            return user;
        }
    }
}
