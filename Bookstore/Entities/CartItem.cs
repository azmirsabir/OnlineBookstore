using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookstore.Entities;

public class CartItem: BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int Quantity { get; set; } = 1;
    
    public Book Book { get; set; } = null!;
    
    [JsonIgnore]
    public User User { get; set; } = null!;
}