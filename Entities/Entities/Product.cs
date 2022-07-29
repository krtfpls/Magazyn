using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Product
    {
       
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string? SerialNumber { get; set; }
        
        [Required]
        [Column(TypeName ="DECIMAL(8,2)")]
        public decimal PriceNetto { get; set; } = 0.01m;

        public int? MinLimit { get; set; }

        [Required]
        public int Quantity {get; set;} = 0;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int CategoryId {get;set;}
        public Category Category { get; set; }
    }
}