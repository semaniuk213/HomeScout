using AutoMapper;
using HomeScout.BLL.DTOs;
using HomeScout.BLL.Exceptions;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HomeScout.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var result = new List<UserDto>();
            foreach (var u in users)
                result.Add(await MapUserToDtoAsync(u));
            return result;
        }

        public async Task<PagedList<UserDto>> GetAllPaginatedAsync(UserParameters parameters)
        {
            var usersPaged = await _unitOfWork.UserRepository
                .GetAllPaginatedAsync(parameters, new SortHelper<User>());

            var dtos = new List<UserDto>();
            foreach (var u in usersPaged)
                dtos.Add(await MapUserToDtoAsync(u));

            return PagedList<UserDto>.Create(
                dtos,
                usersPaged.TotalCount,
                usersPaged.CurrentPage,
                usersPaged.PageSize
            );
        }
        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new UserNotFoundException($"User with id {id} not found.");

            return await MapUserToDtoAsync(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return await MapUserToDtoAsync(user);
        }

        public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var existing = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (existing == null)
                throw new UserNotFoundException($"User with id {id} not found.");

            _mapper.Map(dto, existing);
            _unitOfWork.UserRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return await MapUserToDtoAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (existing == null)
                throw new UserNotFoundException($"User with id {id} not found.");

            _unitOfWork.UserRepository.Remove(existing);
            await _unitOfWork.CompleteAsync();
        }

        private async Task<UserDto> MapUserToDtoAsync(User user)
        {
            var dto = _mapper.Map<UserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            dto.Role = roles.FirstOrDefault() ?? "User";
            return dto;
        }
    }
}
