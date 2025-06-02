using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model.DTOs;

public class CartItemUpdateRequest
{
    [Required]
    [Range(1, 1000000)]
    public int Quantity { get; set; } = 1;
}