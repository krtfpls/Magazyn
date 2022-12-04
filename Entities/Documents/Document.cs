using System.ComponentModel.DataAnnotations;

namespace Entities.Documents
{
    public class Document
    {
     
        public Guid Id { get; set; }

        //Type and typeId
        [Required]
        public int TypeId {get; set;}
        public DocumentType? Type { get; set; } 

        [Required]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

         [Required]
         [StringLength(20)]
        // Number
        public string Number { get; set; }= string.Empty;
        
         [Required]
        // Date
        public DateTime? Date { get; set; } //nullable for required work properly
        // Lines
        public IEnumerable<DocumentLine> DocumentLines { get; set; } = new List<DocumentLine>();
    }
}