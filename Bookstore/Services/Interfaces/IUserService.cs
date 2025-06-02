using Bookstore.Entities;
using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;

namespace Bookstore.Services.Interfaces;

public interface IUserService
{
    Task<PaginatedResult<User>> UsersListAsync(QueryParameters request);
    Task<User> GetUserByIdAsync(int id);
    Task<User> SaveUserAsync(UserDto user);
    Task<User> UpdateUserAsync(int userId,UserDto user);
    Task<bool> DeleteUserAsync(int id);
}