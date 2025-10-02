using AutoMapper;
using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.DAL.Entities;

namespace HomeScout.ListingService.BLL.Profiles
{
    public class FilterProfile : Profile
    {
        public FilterProfile()
        {
            CreateMap<Filter, FilterDto>();
            CreateMap<CreateFilterDto, Filter>();
            CreateMap<UpdateFilterDto, Filter>();
        }
    }
}
