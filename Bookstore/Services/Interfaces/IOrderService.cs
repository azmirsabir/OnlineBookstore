using Bookstore.Entities;
using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;

namespace Bookstore.Services.Interfaces;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(PlaceOrderRequest request, int userId);
    Task<PaginatedResult<Order>> GetAllOrdersAsync(QueryParameters request, User currentUser);
    Task<Order> GetOrderByIdAsync(int orderId, User currentUser);
    Task RejectOrderAsync(int orderId);
    Task ShipOrderAsync(int orderId);
    Task ApproveOrderAsync(int orderId);
}