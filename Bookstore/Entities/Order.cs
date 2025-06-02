using System.ComponentModel.DataAnnotations;
using Bookstore.Model;

namespace Bookstore.Entities;

public class Order: BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }

    [Required] 
    public decimal TotalAmount { get; set; } = 0;
    
    public Decimal Discount { get; set; } = 0;
    [MaxLength(100)]
    public string ShippingAddress { get; set; } = string.Empty;
    public bool IsShipped { get; set; } = false;
    public bool IsApproved { get; set; } = false;
    public bool IsRejected { get; set; } = false;
    [MaxLength(20)]
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

    public List<OrderItem> OrderItems { get; set; } = new();
}