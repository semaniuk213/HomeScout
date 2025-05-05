using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.DAL.Entities;

namespace HomeScout.BLL.Profiles
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
