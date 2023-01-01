using System.ComponentModel.DataAnnotations;

namespace Entities.Documents
{
    public class Document
    {
        public Guid Id { get; set; }

        //Type and typeId
        [Required]
        public DocumentType Type { get; set; } 

        [Required]
        public Customer Customer { get; set; }

         [Required]
         [StringLength(20)]
        // Number
        public string Number { get; set; }
        
         [Required]
        // Date
        public DateOnly Date { get; set; } //nullable for required work properly
        
        [Required]
        public User User { get; set; }
        // Lines
        public IEnumerable<DocumentLine> DocumentLines { get; set; }
    }
}