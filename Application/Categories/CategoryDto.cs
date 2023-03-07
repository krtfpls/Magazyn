using System.ComponentModel.DataAnnotations;

namespace Application.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}