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
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OwnerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<OwnerDto>> GetAllAsync(OwnerParameters parameters, CancellationToken cancellationToken = default)
        {
            var ownersPaged = await _unitOfWork.OwnerRepository
                .GetAllPaginatedAsync(parameters, new SortHelper<Owner>(), cancellationToken);

            var dtos = _mapper.Map<List<OwnerDto>>(ownersPaged);

            return PagedList<OwnerDto>.Create(
                dtos,
                ownersPaged.TotalCount,
                ownersPaged.CurrentPage,
                ownersPaged.PageSize
            );
        }

        public async Task<OwnerDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var owner = await _unitOfWork.OwnerRepository.GetByIdAsync(id, cancellationToken);
            if (owner == null)
                throw new OwnerNotFoundException($"Owner with id {id} not found.");

            return _mapper.Map<OwnerDto>(owner);
        }

        public async Task<OwnerDto> CreateAsync(CreateOwnerDto dto, CancellationToken cancellationToken = default)
        {
            var owner = _mapper.Map<Owner>(dto);
            await _unitOfWork.OwnerRepository.AddAsync(owner, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<OwnerDto>(owner);
        }

        public async Task<OwnerDto> UpdateAsync(int id, UpdateOwnerDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.OwnerRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new OwnerNotFoundException($"Owner with id {id} not found.");

            _mapper.Map(dto, existing);

            _unitOfWork.OwnerRepository.Update(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<OwnerDto>(existing);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.OwnerRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new OwnerNotFoundException($"Owner with id {id} not found.");

            _unitOfWork.OwnerRepository.Remove(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}