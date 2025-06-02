using Bookstore.Data;
using Bookstore.Entities;
using Bookstore.Exceptions;
using Bookstore.Extensions;
using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;
using Bookstore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Services;

public class OrderService(ApplicationDbContext _context): IOrderService
{
    public async Task<Order> PlaceOrderAsync(PlaceOrderRequest request,int userId)
    {
        var items =await  _context.CartItems
            .Include(x => x.Book)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        if (!items.Any()) throw new InvalidOperationException("Cart is Empty.");
        
        var totalAmount = items.Sum(item => item.Quantity * item.Book.Price);
        
        var order = new Order
        {
            UserId = userId,
            TotalAmount = totalAmount,
            Discount = 0,
            ShippingAddress = request.ShippingAddress,
            PaymentMethod = request.PaymentMethod,
            IsApproved = false,
            IsRejected = false,
            IsShipped = false,
            OrderItems = items.Select(item => new OrderItem
            {
                BookId = item.BookId,
                Quantity = item.Quantity,
                UnitPrice = item.Book.Price
            }).ToList()
        };
        
        _context.Orders.Add(order);

        foreach (var item in items)
        {
            item.Book.IsAvailable = false;
        }

        // Remove cart items
        _context.CartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
        
        return order;
    }
    
    public async Task<PaginatedResult<Order>> GetAllOrdersAsync(QueryParameters request, User currentUser)
    {
        var userId = currentUser.Id;
        var userRole = currentUser.Role;
        
        var query = _context.Orders.AsQueryable();
        
        query = userRole != "Admin"
            ? query.Where(o => o.UserId == userId)
            : query;
        
         var orders = await query.ApplySearchAndSort(request)
             .Include(o => o.OrderItems)
            .PaginateAsync(request.PerPage, request.Page);
        
        return orders;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId, User currentUser)
    {
        var userId = currentUser.Id;
        var userRole = currentUser.Role;
        
        var query =_context.Orders.AsQueryable();
        query = userRole != "Admin"
            ? query.Where(o => o.UserId == userId)
            : query;
        
        var order = await query.Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        if(order is null) throw new NotFoundException("Order not found");
        return order;
    }

    public async Task RejectOrderAsync(int orderId)
    {
        var order=await _context.Orders.FindAsync(orderId);
        if(order is null) throw new NotFoundException("Order not found");
        
        order.IsRejected = true;
        await _context.SaveChangesAsync();
    }

    public async Task ShipOrderAsync(int orderId)
    {
        var order=await _context.Orders.FindAsync(orderId);
        if(order is null) throw new NotFoundException("Order not found");
        
        if(!order.IsApproved) throw new NotFoundException("Order is not approved, first accept the order");
        
        order.IsShipped = true;
        await _context.SaveChangesAsync();
    }

    public async Task ApproveOrderAsync(int orderId)
    {
        var order=await _context.Orders.FindAsync(orderId);
        if(order is null) throw new NotFoundException("Order not found");
        
        order.IsApproved = true;
        await _context.SaveChangesAsync();
    }
}