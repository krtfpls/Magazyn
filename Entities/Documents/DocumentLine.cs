using System.ComponentModel.DataAnnotations;

namespace Entities.Documents
{
    public class DocumentLine
    {
        public int Id {get;set;}
        [Required]
        public Guid ProductId {get; set;}
        public Product Product { get; set; }
         [Required]
        public int Quantity { get; set; }
        
    }
}