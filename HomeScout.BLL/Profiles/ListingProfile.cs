using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.DAL.Entities;

namespace HomeScout.BLL.Profiles
{
    public class ListingProfile : Profile
    {
        public ListingProfile()
        {
            CreateMap<Listing, ListingDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.TypeAsString, opt => opt.MapFrom(src => src.Type.ToString()));
            CreateMap<CreateListingDto, Listing>();
            CreateMap<UpdateListingDto, Listing>();
        }
    }
}
