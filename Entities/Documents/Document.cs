using System.ComponentModel.DataAnnotations;

namespace Entities.Documents
{
    public class Document
    {
        public Guid Id { get; set; }

        //Type and typeId
        [Required]
        public int TypeId { get; set; } 
        [Required]
        public DocumentType? Type { get; set; } 

        [Required]
        public int CustomerId { get; set; }
        [Required]
        public Customer? Customer { get; set; }
        
      // Number
         [Required]
         [StringLength(20)]
        public string Number { get; set; }
        
         [Required]
        // Date
        public DateOnly Date { get; set; }
        [Required]
        public string UserId {get; set;}
        
        [Required]
        public User? User { get; set; }
        // Lines
        public IEnumerable<DocumentLine> DocumentLines { get; set; }
    }
}