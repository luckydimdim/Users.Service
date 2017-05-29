using AutoMapper;
using Cmas.BusinessLayers.Users.Entities;
using Cmas.Services.Users.Dtos.Responses;

namespace Cmas.Services.Users
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, SimpleUserResponse>();
            CreateMap<User, DetailedUserResponse>();
        }
    }
}
