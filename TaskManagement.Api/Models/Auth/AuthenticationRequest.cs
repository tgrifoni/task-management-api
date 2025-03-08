using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Models.Auth;

public record AuthenticationRequest
{
   [Required]
   [StringLength(maximumLength: 50, MinimumLength = 5)]
   public string Username { get; init; } = string.Empty;

   [Required]
   [StringLength(maximumLength: 50, MinimumLength = 5)]
   public string Password { get; init; } = string.Empty;
}
