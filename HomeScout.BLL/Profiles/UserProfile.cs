using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.DAL.Entities;

namespace HomeScout.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
