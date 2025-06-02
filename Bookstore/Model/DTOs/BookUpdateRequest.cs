
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model.DTOs;

public class BookUpdateRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(50, ErrorMessage = "Title can not be more than 50 characters long.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Author is required.")]
    [MaxLength(50, ErrorMessage = "Author can not be more than 50 characters long.")]
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; }= string.Empty;
    
    [Required(ErrorMessage = "Price is required.")]
    [Range(0.0, 9999999999.0)]
    public decimal Price { get; set; } = 0;
    public bool IsAvailable { get; set; } = true;
}