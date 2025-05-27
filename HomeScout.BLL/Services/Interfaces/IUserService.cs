﻿using HomeScout.BLL.DTOs;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;

namespace HomeScout.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<PagedList<UserDto>> GetAllPaginatedAsync(UserParameters parameters);
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<UserDto> UpdateAsync(string id, UpdateUserDto dto);
        Task DeleteAsync(string id);
    }
}
