using System.ComponentModel.DataAnnotations;

namespace Application.Categories
{
    public class CategoryDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}