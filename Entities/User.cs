using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User: IdentityUser
    {

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }= string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName {get; set;}= string.Empty;

  
    }
}