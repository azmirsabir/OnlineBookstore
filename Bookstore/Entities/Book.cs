using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookstore.Entities;

public class Book: BaseEntity
{ 
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Author { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string Genre { get; set; } = string.Empty;

    [Required] 
    [Range(0.0, 9999999999.0)] public decimal Price { get; set; } = 0;

    [Required] 
    public bool IsAvailable { get; set; } = true;
    
    [JsonIgnore]
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    
    [JsonIgnore]
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}