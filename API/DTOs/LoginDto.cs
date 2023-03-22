using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {   [Required]
        [MinLength(3), MaxLength(100)]
        public string Username { get; set; }
        
        [Required]
        [MinLength(3), MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$")]
        public string Password { get; set; }
    }
}