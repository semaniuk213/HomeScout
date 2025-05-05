using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;

namespace HomeScout.BLL.Services
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

        public async Task<IEnumerable<FilterDto>> GetAllAsync()
        {
            var filters = await _unitOfWork.FilterRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FilterDto>>(filters);
        }

        public async Task<FilterDto?> GetByIdAsync(int id)
        {
            var filter = await _unitOfWork.FilterRepository.GetByIdAsync(id);
            return filter == null ? null : _mapper.Map<FilterDto>(filter);
        }

        public async Task<FilterDto> CreateAsync(CreateFilterDto dto)
        {
            var filter = _mapper.Map<Filter>(dto);
            await _unitOfWork.FilterRepository.AddAsync(filter);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<FilterDto>(filter);
        }

        public async Task<FilterDto?> UpdateAsync(UpdateFilterDto dto)
        {
            var existing = await _unitOfWork.FilterRepository.GetByIdAsync(dto.Id);
            if (existing == null)
                return null;

            _mapper.Map(dto, existing);
            _unitOfWork.FilterRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<FilterDto>(existing);
        }

        public async Task<PagedList<FilterDto>> GetFilteredAsync(FilterParameters parameters)
        {
            var filters = await _unitOfWork.FilterRepository.GetFilteredAsync(parameters, new SortHelper<Filter>());
            return PagedList<FilterDto>.Create(
                _mapper.Map<List<FilterDto>>(filters),
                filters.TotalCount,
                filters.CurrentPage,
                filters.PageSize
            );
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _unitOfWork.FilterRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _unitOfWork.FilterRepository.Remove(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
