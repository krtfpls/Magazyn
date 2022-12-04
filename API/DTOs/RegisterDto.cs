using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Nie podano nazwy użytkownika")]
        public string Username {get;set;}

        [Required(ErrorMessage ="Nie podano hasła")]
        [MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$", ErrorMessage ="Hasło musi zawierać dużą, małą literę oraz cyfrę oraz byc w przedziale 8-50 znaków")]
        public string Password { get; set; }
    }
}