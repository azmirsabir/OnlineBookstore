using Bookstore.Entities;
using Bookstore.Model.Dtos;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;

namespace Bookstore.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterAsync(UserDto request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RefreshTokensAsync(RefreshTokenRequest request);
}