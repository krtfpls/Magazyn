using System.ComponentModel.DataAnnotations;
using Entities.Documents;
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

        public IEnumerable<Product> Products { get; set; } 

        public IEnumerable<Document> Documents {get; set;}
        
    }
}