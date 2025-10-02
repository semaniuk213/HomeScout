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
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<PhotoDto>> GetAllAsync(PhotoParameters parameters, CancellationToken cancellationToken = default)
        {
            var pagedPhotos = await _unitOfWork.PhotoRepository.GetAllPaginatedAsync(parameters, new SortHelper<Photo>(), cancellationToken);

            return PagedList<PhotoDto>.Create(
                _mapper.Map<List<PhotoDto>>(pagedPhotos),
                pagedPhotos.TotalCount,
                pagedPhotos.CurrentPage,
                pagedPhotos.PageSize
            );
        }

        public async Task<PhotoDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var photo = await _unitOfWork.PhotoRepository.GetByIdAsync(id, cancellationToken);
            if (photo == null)
                throw new PhotoNotFoundException($"Photo with id {id} not found.");

            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> CreateAsync(CreatePhotoDto dto, CancellationToken cancellationToken = default)
        {
            var photo = _mapper.Map<Photo>(dto);
            await _unitOfWork.PhotoRepository.AddAsync(photo, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> UpdateAsync(int id, UpdatePhotoDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.PhotoRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new PhotoNotFoundException($"Photo with id {id} not found.");

            _mapper.Map(dto, existing);

            _unitOfWork.PhotoRepository.Update(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<PhotoDto>(existing);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _unitOfWork.PhotoRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new PhotoNotFoundException($"Photo with id {id} not found.");

            _unitOfWork.PhotoRepository.Remove(existing);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}