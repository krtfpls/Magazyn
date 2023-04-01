using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ResetPasswordDto
    {
        
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$", ErrorMessage ="The password must contain a capital letter, a small letter and a number and must be between 8 and 50 characters")]
        public string Password { get; set; }

        // [DataType(DataType.Password)]
        // [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        // public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Token { get; set; }
    }
}