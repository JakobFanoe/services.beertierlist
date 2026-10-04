using System.ComponentModel.DataAnnotations;

namespace services.beertierlist.api.Controllers.Account.Requests;

public class RegisterRequest
{
    [Required]
    [StringLength(256, MinimumLength = 3)]
    public required string Username { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 12)]
    public required string Password { get; set; }
}