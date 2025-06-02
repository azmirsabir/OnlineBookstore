using System.Security.Claims;
using Bookstore.Entities;
using Bookstore.Helpers;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

[ApiController]
[Route("[controller]")]
public class CartItemController(ICartService _cartService): ControllerBase
{
    [HttpGet("/checkout/{userId}")]
    [Authorize]
    public async Task<ActionResult<Response<List<CartItem>>>> Checkout([FromRoute] int userId)
    {
        var items = await _cartService.CheckoutAsync(userId);
        return Ok(new Response<List<CartItem>>("Checkout items",items));
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Response<CartItem>>> StoreCartItem(AddToCartItemRequest itemRequest)
    {
        var user = JWT.GetCurrentUser(User);
        var item = await _cartService.AddToCartAsync(user, itemRequest);
        return Ok(new Response<CartItem>("Card Item",item));
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<CartItem>> UpdateCart([FromRoute] int id,CartItemUpdateRequest request)
    {
        var user = JWT.GetCurrentUser(User);
        var item = await _cartService.UpdateCartItem(id,user, request);
        return Ok(new Response<CartItem>("Card Item",item));
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCartItem([FromRoute] int id)
    {
        var user = JWT.GetCurrentUser(User);
        await _cartService.DeleteItemAsync(id, user);
        return Ok(new Response<string>("Card Item deleted successfully"));
    }
}