using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User = Bookstore.Entities.User;

namespace Bookstore.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("[controller]")]
public class UserController(IUserService _userService): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<User>>> SearchUsers([FromQuery] QueryParameters request)
    {
        var users = await _userService.UsersListAsync(request);
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Response<User>>> GetUserById([FromRoute] int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(new Response<User>("User found",user));    
    }
    
    [HttpPost]
    public async Task<ActionResult<Response<User>>> StoreUser(UserDto request)
    {
        var user = await _userService.SaveUserAsync(request);
        return Ok(new Response<User>("User saved successfully",user));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Response<User>>> UpdateUser([FromRoute] int id,UserDto user)
    {
        var updatedUser=await _userService.UpdateUserAsync(id,user);
        return Ok(new Response<User>("User updated successfully",updatedUser));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<string>>> DeleteUser([FromRoute] int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok(new Response<string>($"User Id {id} deleted"));
    }
}