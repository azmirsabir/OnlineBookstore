using Bookstore.Entities;
using Bookstore.Model.Dtos;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService _authService): ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<Response<LoginResponse>>> Login(LoginRequest request)
    {
        var auth = await _authService.LoginAsync(request);
        return Ok(new Response<LoginResponse>("Login successful ",auth));
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<Response<User>>> Register(UserDto request)
    {
        var user=await _authService.RegisterAsync(request);
        return Ok(new Response<User>("Register successful", user));
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<LoginResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var auth=await _authService.RefreshTokensAsync(request);
        return Ok(new Response<LoginResponse>("Token refreshed successfully",auth));
    }
}
