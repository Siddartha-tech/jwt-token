using System.ComponentModel.DataAnnotations;

namespace jtw_token.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}