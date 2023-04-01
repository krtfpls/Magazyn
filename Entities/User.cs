using System.ComponentModel.DataAnnotations;
using Entities.Documents;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User: IdentityUser
    {
        
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName {get; set;}

        public IEnumerable<Product> Products { get; set; } 

        public IEnumerable<Document> Documents {get; set;}
        
    }
}