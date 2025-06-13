using System.ComponentModel.DataAnnotations;

namespace MoneyOrbit.Application.DTOs.AuthDtos
{
    /// <summary>
    /// Data Transfer Object for submitting a new password with a reset token.
    /// </summary>
    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The new password must be at least 8 characters long.")]
        public string NewPassword { get; set; }
    }
}