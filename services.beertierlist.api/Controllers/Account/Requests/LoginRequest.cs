using System.ComponentModel.DataAnnotations;

namespace services.beertierlist.api.Controllers.Account.Requests;

public class LoginRequest
{
    [Required]
    [StringLength(256)]
    public required string Username { get; set; }

    [Required]
    [StringLength(128)]
    public required string Password { get; set; }
}