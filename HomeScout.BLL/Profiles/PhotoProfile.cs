using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.DAL.Entities;

namespace HomeScout.BLL.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoDto>()
                .ForMember(dest => dest.ListingTitle, opt => opt.MapFrom(src => src.Listing.Title));

            CreateMap<CreatePhotoDto, Photo>();
            CreateMap<UpdatePhotoDto, Photo>();
        }
    }
}
