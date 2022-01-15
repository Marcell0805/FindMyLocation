using System.ComponentModel.DataAnnotations;

namespace FindMyLocation.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
