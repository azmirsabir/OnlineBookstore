using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model.DTOs;

public class AddToCartItemRequest
{ 
    [Required(ErrorMessage = "Password is required.")]
    public int BookId { get; set; }
    
    [Range(0.0, 9999999999.0)] 
    public decimal Price { get; set; } = 0;
    
    [Range(1, 1000000)] 
    public int Quantity { get; set; }
}