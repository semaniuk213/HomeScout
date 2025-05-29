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
    public class ListingService : IListingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListingDto>> GetAllAsync()
        {
            var listings = await _unitOfWork.ListingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ListingDto>>(listings);
        }

        public async Task<PagedList<ListingDto>> GetAllPaginatedAsync(ListingParameters parameters)
        {
            var pagedListings = await _unitOfWork.ListingRepository.GetAllPaginatedAsync(parameters, new SortHelper<Listing>());
            return PagedList<ListingDto>.Create(
                _mapper.Map<List<ListingDto>>(pagedListings),
                pagedListings.TotalCount,
                pagedListings.CurrentPage,
                pagedListings.PageSize
            );
        }

        public async Task<ListingDto> GetByIdAsync(int id)
        {
            var listing = await _unitOfWork.ListingRepository.GetByIdAsync(id);
            if (listing == null)
                throw new ListingNotFoundException($"Listing with id {id} not found.");

            return _mapper.Map<ListingDto>(listing);
        }

        public async Task<IEnumerable<ListingDto>> GetByUserIdAsync(Guid userId)
        {
            var listings = await _unitOfWork.ListingRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<ListingDto>>(listings);
        }

        public async Task<ListingDto> CreateAsync(CreateListingDto dto)
        {
            if (!Enum.TryParse<ListingType>(dto.Type, true, out var typeEnum))
                throw new ArgumentException("Invalid listing type. Allowed values: Rent or Sale.");

            var listing = _mapper.Map<Listing>(dto);
            listing.Type = typeEnum;

            await _unitOfWork.ListingRepository.AddAsync(listing);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ListingDto>(listing);
        }

        public async Task<ListingDto> UpdateAsync(int id, UpdateListingDto dto)
        {
            var existing = await _unitOfWork.ListingRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ListingNotFoundException($"Listing with id {id} not found.");

            if (!Enum.TryParse<ListingType>(dto.Type, true, out var typeEnum))
                throw new ArgumentException("Invalid listing type. Allowed values: Rent or Sale.");

            _mapper.Map(dto, existing);
            existing.Type = typeEnum;

            _unitOfWork.ListingRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ListingDto>(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _unitOfWork.ListingRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ListingNotFoundException($"Listing with id {id} not found.");

            _unitOfWork.ListingRepository.Remove(existing);
            await _unitOfWork.CompleteAsync();
        }
    }
}
