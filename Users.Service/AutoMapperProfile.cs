using AutoMapper;
using Cmas.BusinessLayers.Users.Entities;
using Cmas.Infrastructure.Security;
using Cmas.Services.Users.Dtos.Requests;
using Cmas.Services.Users.Dtos.Responses;

namespace Cmas.Services.Users
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, SimpleUserResponse>();
            CreateMap<User, DetailedUserResponse>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserRequest>();
            CreateMap<Role, string>().ConvertUsing(src => src.ToString().ToUpper());
        }
    }
}
