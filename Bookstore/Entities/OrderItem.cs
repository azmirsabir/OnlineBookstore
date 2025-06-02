using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookstore.Entities;

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public decimal UnitPrice { get; set; }

    public Book Book { get; set; } = null!;
    
    [JsonIgnore]
    public Order Order { get; set; } = null!;
}