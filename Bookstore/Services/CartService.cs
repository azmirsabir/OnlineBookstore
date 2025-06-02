using System.ComponentModel.DataAnnotations;
using Bookstore.Data;
using Bookstore.Entities;
using Bookstore.Exceptions;
using Bookstore.Model.DTOs;
using Bookstore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Services;

public class CartService(ApplicationDbContext _context): ICartService
{
    public async Task<CartItem> AddToCartAsync(User user, AddToCartItemRequest request) {
        var userId = user.Id;
        var book = await _context.Books
            .Where(b => b.Id == request.BookId)
            .Select(b => new { b.Id, b.IsAvailable })
            .FirstOrDefaultAsync();
        
        if (book == null || !book.IsAvailable)
        {
            throw new NotFoundException("Book not found or is not available");
        }
        
        var item = await _context.CartItems
            .FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == request.BookId);
        
        if (item != null)
        {
            item.Quantity += request.Quantity;
        }
        else
        {
            item = new CartItem
            {
                UserId = userId,
                BookId = request.BookId,
                Quantity = request.Quantity
            };
            _context.CartItems.Add(item);
        }
        
        await _context.SaveChangesAsync();
        return item;
    }
    public async Task<List<CartItem>> CheckoutAsync(int userId) {
        return await _context.CartItems.Where(c => c.UserId == userId)
            .Include(o=>o.Book)
            .Include(o1=>o1.User)
            .ToListAsync();
    }
    public async Task<CartItem> UpdateCartItem(int id, User user, CartItemUpdateRequest request)
    {
        var userId = user.Id;
        
        var cartItem = await _context.CartItems
            .Include(c => c.Book)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);

        if (cartItem == null)
        {
            throw new NotFoundException("Item not found");
        }

        // If quantity increases, ensure book is still available
        if (request.Quantity > cartItem.Quantity && !cartItem.Book.IsAvailable)
        { 
            throw new ValidationException("Book is not available");
        }

        cartItem.Quantity = request.Quantity;
        await _context.SaveChangesAsync();

        return cartItem;
    }
    public async Task<bool> DeleteItemAsync(int id, User user) {
        int userId = user.Id;
        var item = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
        
        if (item == null) throw new NotFoundException("Item not found");

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}