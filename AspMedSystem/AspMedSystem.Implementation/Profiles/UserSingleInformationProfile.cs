using AspMedSystem.Application.DTO;
using AspMedSystem.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.Profiles
{
    public class UserSingleInformationProfile : Profile
    {
        public UserSingleInformationProfile() 
        {
            CreateMap<User, UserSearchSingleInformationDTO>()
                .ForMember(user => user.Group, user => user.MapFrom(queryUser => queryUser.Group.Name));
        }
    }
}
