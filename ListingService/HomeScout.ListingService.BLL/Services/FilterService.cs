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
    public class FilterService : IFilterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FilterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<FilterDto>> GetAllAsync(FilterParameters parameters, CancellationToken cancellationToken = default)
        {
            var filters = await _unitOfWork.FilterRepository
                .GetFilteredAsync(parameters, new SortHelper<Filter>(), cancellationToken);

            return PagedList<FilterDto>.Create(
                _mapper.Map<List<FilterDto>>(filters),
                filters.TotalCount,
                filters.CurrentPage,
                filters.PageSize
            );
        }

        public async Task<FilterDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var filter = await _unitOfWork.FilterRepository.GetByIdAsync(id, cancellationToken);
            if (filter == null)
                throw new FilterNotFoundException($"Filter with id {id} not found.");

            return _mapper.Map<FilterDto>(filter);
        }

        public async Task<FilterDto> CreateAsync(CreateFilterDto dto, CancellationToken cancellationToken = default)
        {
            var filter = _mapper.Map<Filter>(dto);
            await _unitOfWork.FilterRepository.AddAsync(filter, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return _mapper.Map<FilterDto>(filter);
        }

        public async Task<FilterDto> UpdateAsync(int id, UpdateFilterDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.FilterRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new FilterNotFoundException($"Filter with id {id} not found.");

            _mapper.Map(dto, existing);
            _unitOfWork.FilterRepository.Update(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<FilterDto>(existing);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.FilterRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new FilterNotFoundException($"Filter with id {id} not found.");

            _unitOfWork.FilterRepository.Remove(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}