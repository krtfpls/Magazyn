using System.ComponentModel.DataAnnotations;

namespace Entities.Documents
{
    public class Customer
    {
        public int Id {get;set;}

        [Required]
        [StringLength(100)]
        public string Name {get; set;}

        [Required]
        [StringLength(100)]
        public string Street {get;set;}
    
        [Required]
        [StringLength(100)]
        public string StreetNumber {get;set;}
        
        [Required]
        [StringLength(100)]
        public string City {get;set;}

        [StringLength(100)]
        public string? TaxNumber {get;set;}

        [StringLength(500)]
        public string? Description {get;set;}

        public IEnumerable<Document> Documents {get; set;}
    }
}