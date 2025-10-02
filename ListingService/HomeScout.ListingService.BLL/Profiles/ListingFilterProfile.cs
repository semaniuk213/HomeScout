using AutoMapper;
using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Entities;

namespace HomeScout.ListingService.BLL.Profiles
{
    public class ListingFilterProfile : Profile
    {
        public ListingFilterProfile()
        {
            CreateMap<ListingFilter, ListingFilterDto>()
                .ForMember(dest => dest.ListingTitle, opt => opt.MapFrom(src => src.Listing != null ? src.Listing.Title : string.Empty))
                .ForMember(dest => dest.FilterName, opt => opt.MapFrom(src => src.Filter != null ? src.Filter.Name : string.Empty));

            CreateMap<CreateListingFilterDto, ListingFilter>();
            CreateMap<UpdateListingFilterDto, ListingFilter>();
        }
    }
}
