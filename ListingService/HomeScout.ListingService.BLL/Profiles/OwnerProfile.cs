using AutoMapper;
using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Entities;

namespace HomeScout.ListingService.BLL.Profiles
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Owner, OwnerDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email ?? string.Empty))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? string.Empty));

            CreateMap<CreateOwnerDto, Owner>();

            CreateMap<UpdateOwnerDto, Owner>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
