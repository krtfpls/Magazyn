using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$", ErrorMessage ="The password must contain a capital letter, a small letter and a number and must be between 8 and 50 characters")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(100)]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$", ErrorMessage ="The password must contain a capital letter, a small letter and a number and must be between 8 and 50 characters")]
        public string Password { get; set; }


    }
}