using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {   
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email {get;set;}
        
        [Required]
        [MinLength(3), MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$")]
        public string Password { get; set; }
    }
}