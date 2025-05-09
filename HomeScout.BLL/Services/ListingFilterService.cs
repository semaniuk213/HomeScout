using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.BLL.Exceptions;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;

namespace HomeScout.BLL.Services
{
    public class ListingFilterService : IListingFilterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListingFilterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListingFilterDto>> GetByListingIdAsync(int listingId)
        {
            var listingFilters = await _unitOfWork.ListingFilterRepository.GetByListingIdAsync(listingId);
            return _mapper.Map<IEnumerable<ListingFilterDto>>(listingFilters);
        }

        public async Task<IEnumerable<ListingFilterDto>> GetByFilterIdAsync(int filterId)
        {
            var listingFilters = await _unitOfWork.ListingFilterRepository.GetByFilterIdAsync(filterId);
            return _mapper.Map<IEnumerable<ListingFilterDto>>(listingFilters);
        }

        public async Task<PagedList<ListingFilterDto>> GetAllPaginatedAsync(ListingFilterParameters parameters)
        {
            var pagedListingFilters = await _unitOfWork.ListingFilterRepository.GetAllPaginatedAsync(parameters, new SortHelper<ListingFilter>());
            return PagedList<ListingFilterDto>.Create(
                _mapper.Map<List<ListingFilterDto>>(pagedListingFilters),
                pagedListingFilters.TotalCount,
                pagedListingFilters.CurrentPage,
                pagedListingFilters.PageSize
            );
        }

        public async Task<ListingFilterDto> CreateAsync(CreateListingFilterDto dto)
        {
            var listingFilter = _mapper.Map<ListingFilter>(dto);
            await _unitOfWork.ListingFilterRepository.AddAsync(listingFilter);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ListingFilterDto>(listingFilter);
        }

        public async Task<ListingFilterDto> UpdateAsync(int id, UpdateListingFilterDto dto)
        {
            var existing = await _unitOfWork.ListingFilterRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ListingFilterNotFoundException($"ListingFilter with id {id} not found.");

            _mapper.Map(dto, existing);
            _unitOfWork.ListingFilterRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ListingFilterDto>(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _unitOfWork.ListingFilterRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ListingFilterNotFoundException($"ListingFilter with id {id} not found.");

            _unitOfWork.ListingFilterRepository.Remove(existing);
            await _unitOfWork.CompleteAsync();
        }
    }
}
