using Bookstore.Entities;
using Bookstore.Model.DTOs;

namespace Bookstore.Services.Interfaces;

public interface ICartService
{
    Task<CartItem> AddToCartAsync(User user, AddToCartItemRequest itemRequest);
    Task<List<CartItem>> CheckoutAsync(int userId);
    public Task<CartItem> UpdateCartItem(int id, User user, CartItemUpdateRequest request);
    public Task<bool> DeleteItemAsync(int id,User user);
}