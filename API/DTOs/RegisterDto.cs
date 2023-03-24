using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="No User name")]
        [MinLength(3), MaxLength(100)]
        public string Username {get;set;}

        [Required(ErrorMessage ="No Password")]
        [MinLength(3), MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$", ErrorMessage ="Hasło musi zawierać dużą, małą literę oraz cyfrę oraz byc w przedziale 8-50 znaków")]
        public string Password { get; set; }
    }
}