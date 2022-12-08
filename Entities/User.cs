using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
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

        public IEnumerable<Product>? Products { get; set; }
        [NotMapped]
        public IEnumerable<Document>? Documents { get; set; }
    }
}