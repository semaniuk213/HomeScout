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
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.Name))
                .ForMember(dest => dest.TypeAsString, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<CreateListingDto, Listing>()
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));

            CreateMap<UpdateListingDto, Listing>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}