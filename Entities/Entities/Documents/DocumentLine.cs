using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.Documents
{
    public class DocumentLine
    {
        public int Id {get;set;}
        [Required]
        public Product Product { get; set; }
         [Required]
        public int Quantity { get; set; }
        
    }
}