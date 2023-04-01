using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="No User name")]
        [MinLength(3), MaxLength(100)]
        public string UserName {get;set;}

        //[Required(ErrorMessage ="No User FirstName")]
        [MaxLength(100)]
        public string FirstName {get;set;}

        //[Required(ErrorMessage ="No User LastName")]
        [MaxLength(100)]
        public string LastName {get;set;}

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="No Password")]
        [MinLength(3), MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$", ErrorMessage ="The password must contain a capital letter, a small letter and a number and must be between 8 and 50 characters")]
        public string Password { get; set; }
    }
}