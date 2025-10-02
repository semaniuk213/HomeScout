using AutoMapper;
using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.BLL.Exceptions;
using HomeScout.ListingService.BLL.Services.Interfaces;
using HomeScout.ListingService.DAL.Entities;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;
using HomeScout.ListingService.DAL.Repositories.Interfaces;

namespace HomeScout.ListingService.BLL.Services
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

        public async Task<PagedList<ListingDto>> GetAllAsync(ListingParameters parameters, CancellationToken cancellationToken = default)
        {
            var pagedListings = await _unitOfWork.ListingRepository
                .GetAllPaginatedAsync(parameters, new SortHelper<Listing>(), cancellationToken);

            return PagedList<ListingDto>.Create(
                _mapper.Map<List<ListingDto>>(pagedListings),
                pagedListings.TotalCount,
                pagedListings.CurrentPage,
                pagedListings.PageSize
            );
        }

        public async Task<ListingDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var listing = await _unitOfWork.ListingRepository.GetByIdAsync(id, cancellationToken);
            if (listing == null)
                throw new ListingNotFoundException($"Listing with id {id} not found.");

            return _mapper.Map<ListingDto>(listing);
        }

        public async Task<ListingDto> CreateAsync(CreateListingDto dto, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<ListingType>(dto.Type, true, out var typeEnum))
                throw new ArgumentException("Invalid listing type. Allowed values: Rent or Sale.");

            var listing = _mapper.Map<Listing>(dto);
            listing.Type = typeEnum;

            await _unitOfWork.ListingRepository.AddAsync(listing, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<ListingDto>(listing);
        }

        public async Task<ListingDto> UpdateAsync(int id, UpdateListingDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.ListingRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new ListingNotFoundException($"Listing with id {id} not found.");

            if (dto.Title != null) existing.Title = dto.Title;
            if (dto.Description != null) existing.Description = dto.Description;
            if (dto.Address != null) existing.Address = dto.Address;
            if (dto.City != null) existing.City = dto.City;
            if (dto.Price.HasValue) existing.Price = dto.Price.Value;
            if (dto.Area.HasValue) existing.Area = dto.Area.Value;
            if (dto.OwnerId.HasValue) existing.OwnerId = dto.OwnerId.Value;

            if (!string.IsNullOrWhiteSpace(dto.Type))
            {
                if (!Enum.TryParse<ListingType>(dto.Type, true, out var typeEnum))
                    throw new ArgumentException("Invalid listing type. Allowed values: Rent or Sale.");
                existing.Type = typeEnum;
            }

            _unitOfWork.ListingRepository.Update(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<ListingDto>(existing);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.ListingRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new ListingNotFoundException($"Listing with id {id} not found.");

            _unitOfWork.ListingRepository.Remove(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}