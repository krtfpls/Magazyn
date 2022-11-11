using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Documents
{
    public class DocumentType
    {
        public int Id {get; set;}

         [Required]
         [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
         [Required]
        public bool isIncomeType { get; set; }
    }
}