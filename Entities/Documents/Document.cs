using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Documents
{
    public class Document
    {
     
        public Guid Id { get; set; }

        //Type and typeId
        [Required]
        public int TypeId {get; set;}
        public DocumentType Type { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

         [Required]
         [StringLength(20)]
        // Number
        public string Number { get; set; }
        
         [Required]
        // Date
        public DateTime Date { get; set; } 
        // Lines
        public IEnumerable<DocumentLine> DocumentLines { get; set; } = new List<DocumentLine>();
    }
}