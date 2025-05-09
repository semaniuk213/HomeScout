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
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhotoDto>> GetAllAsync()
        {
            var photos = await _unitOfWork.PhotoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async Task<IEnumerable<PhotoDto>> GetByListingIdAsync(int listingId)
        {
            var photos = await _unitOfWork.PhotoRepository.GetByListingIdAsync(listingId);
            return _mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async Task<PagedList<PhotoDto>> GetAllPaginatedAsync(PhotoParameters parameters)
        {
            var pagedPhotos = await _unitOfWork.PhotoRepository.GetAllPaginatedAsync(parameters, new SortHelper<Photo>());
            return PagedList<PhotoDto>.Create(
                _mapper.Map<List<PhotoDto>>(pagedPhotos),
                pagedPhotos.TotalCount,
                pagedPhotos.CurrentPage,
                pagedPhotos.PageSize
            );
        }

        public async Task<PhotoDto> GetByIdAsync(int id)
        {
            var photo = await _unitOfWork.PhotoRepository.GetByIdAsync(id);
            if (photo == null)
                throw new PhotoNotFoundException($"Photo with id {id} not found.");

            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> CreateAsync(CreatePhotoDto dto)
        {
            var photo = _mapper.Map<Photo>(dto);
            await _unitOfWork.PhotoRepository.AddAsync(photo);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> UpdateAsync(int id, UpdatePhotoDto dto)
        {
            var existing = await _unitOfWork.PhotoRepository.GetByIdAsync(id);
            if (existing == null)
                throw new PhotoNotFoundException($"Photo with id {id} not found.");

            _mapper.Map(dto, existing);
            _unitOfWork.PhotoRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PhotoDto>(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _unitOfWork.PhotoRepository.GetByIdAsync(id);
            if (existing == null)
                throw new PhotoNotFoundException($"Photo with id {id} not found.");

            _unitOfWork.PhotoRepository.Remove(existing);
            await _unitOfWork.CompleteAsync();
        }
    }
}
