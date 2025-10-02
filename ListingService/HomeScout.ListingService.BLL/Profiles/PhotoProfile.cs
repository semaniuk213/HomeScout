using AutoMapper;
using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Entities;

namespace HomeScout.ListingService.BLL.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoDto>()
                .ForMember(dest => dest.ListingTitle, opt => opt.MapFrom(src => src.Listing != null ? src.Listing.Title : string.Empty));

            CreateMap<CreatePhotoDto, Photo>();
            CreateMap<UpdatePhotoDto, Photo>();
        }
    }
}
