using System.ComponentModel.DataAnnotations;

namespace MoneyOrbit.Application.DTOs.AuthDtos
{
    /// <summary>
    /// Data Transfer Object for initiating a password reset request.
    /// </summary>
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
