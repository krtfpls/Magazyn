using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public IEnumerable<Product> Products {get; set;}
    }
}