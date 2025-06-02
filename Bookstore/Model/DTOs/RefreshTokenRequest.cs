using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model.DTOs;

public class RefreshTokenRequest
{
    [Required]
    public int userId { get; set; }
    
    [Required]
    public required string refreshToken { get; set; }
}