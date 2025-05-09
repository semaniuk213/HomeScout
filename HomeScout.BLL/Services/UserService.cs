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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<PagedList<UserDto>> GetAllPaginatedAsync(UserParameters parameters)
        {
            var users = await _unitOfWork.UserRepository.GetAllPaginatedAsync(parameters, new SortHelper<User>());
            return PagedList<UserDto>.Create(
                _mapper.Map<List<UserDto>>(users),
                users.TotalCount,
                users.CurrentPage,
                users.PageSize
            );
        }

        public async Task<UserDto> GetByUserNameAsync(string userName)
        {
            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);
            if (user == null)
                throw new UserNotFoundException($"User with username {userName} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetByRoleAsync(string role)
        {
            var users = await _unitOfWork.UserRepository.GetByRoleAsync(role);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new UserNotFoundException($"User with id {id} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(string id, UpdateUserDto dto)
        {
            var existing = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (existing == null)
                throw new UserNotFoundException($"User with id {id} not found.");

            _mapper.Map(dto, existing);
            _unitOfWork.UserRepository.Update(existing);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserDto>(existing);
        }

        public async Task DeleteAsync(string id)
        {
            var existing = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (existing == null)
                throw new UserNotFoundException($"User with id {id} not found.");

            _unitOfWork.UserRepository.Remove(existing);
            await _unitOfWork.CompleteAsync();
        }
    }
}
