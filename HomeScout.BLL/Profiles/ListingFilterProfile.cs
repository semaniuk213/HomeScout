using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.DAL.Entities;

namespace HomeScout.BLL.Profiles
{
    public class ListingFilterProfile : Profile
    {
        public ListingFilterProfile()
        {
            CreateMap<ListingFilter, ListingFilterDto>()
                .ForMember(dest => dest.ListingTitle, opt => opt.MapFrom(src => src.Listing.Title))  
                .ForMember(dest => dest.FilterName, opt => opt.MapFrom(src => src.Filter.Name)); 

            CreateMap<CreateListingFilterDto, ListingFilter>();
            CreateMap<UpdateListingFilterDto, ListingFilter>();
        }
    }
}
