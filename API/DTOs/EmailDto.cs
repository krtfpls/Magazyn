using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class EmailDto
    {
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email {get;set;}

        [MaxLength(1000)]
        public string Token { get; set; }
    }
}