using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model.DTOs;

public class PlaceOrderRequest
{
    public string ShippingAddress { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Please enter a valid payment method, accepted payments are: " +
                             "FIB, ZainCash, QI, MasterCard, VisaCard, WesternUnion, ApplyPay, Cash")]
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
}