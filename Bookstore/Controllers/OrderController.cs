using Bookstore.Entities;
using Bookstore.Helpers;
using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

[ApiController]
public class OrderController(IOrderService _orderService) : ControllerBase
{
    [HttpPost("place-order")]
    [Authorize]
    public async Task<ActionResult<Response<Order>>> PlaceOrder(PlaceOrderRequest request)
    {
        var user = JWT.GetCurrentUser(User);
        var order = await _orderService.PlaceOrderAsync(request,user.Id);
        return Ok(new Response<Order>("Order is placed",order));
    }
    
    [HttpGet("orders")]
    [Authorize]
    public async Task<ActionResult<PaginatedResult<Order>>> GetAllOrders([FromQuery]QueryParameters request)
    {
        var user = JWT.GetCurrentUser(User);
        var orders = await _orderService.GetAllOrdersAsync(request,user);
        return Ok(new Response<PaginatedResult<Order>>("List of Paginated Orders",orders));
    }
    
    [HttpGet("orders/{orderId}")]
    [Authorize]
    public async Task<ActionResult<Order>> GetOrderByIdAsync([FromRoute] int orderId)
    {
        var user = JWT.GetCurrentUser(User);
        var order = await _orderService.GetOrderByIdAsync(orderId,user);
        return Ok(new Response<Order>("Order",order));
    }
    
    [HttpPatch("orders/{orderId}/accept")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response<string>>> AcceptOrder([FromRoute] int orderId)
    {
        await _orderService.ApproveOrderAsync(orderId);
        return Ok(new Response<string>($"Order {orderId} Accepted."));
    }
    
    [HttpPatch("orders/{orderId}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response<string>>> RejectOrder([FromRoute] int orderId)
    {
        await _orderService.RejectOrderAsync(orderId);
        return Ok(new Response<string>($"Order {orderId} rejected."));
    }
    
    [HttpPatch("orders/{orderId}/ship")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response<string>>> ShipOrder([FromRoute] int orderId)
    {
        await _orderService.ShipOrderAsync(orderId);
        return Ok(new Response<string>($"Order {orderId} rejected."));
    }
}