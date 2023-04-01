using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserProfileDto
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

    }
}