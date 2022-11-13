using System.ComponentModel.DataAnnotations;

namespace Entities.Documents
{
    public class Customer
    {
        public int Id {get;set;}

        [Required]
        [StringLength(100)]
        public string Name {get; set;}= string.Empty;

        [Required]
        [StringLength(100)]
        public string Street {get;set;} = string.Empty;
    
        [Required]
        [StringLength(100)]
        public string StreetNumber {get;set;} = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string City {get;set;} = string.Empty;

        [StringLength(100)]
        public string? TaxNumber {get;set;}

        [StringLength(500)]
        public string? Description {get;set;}

        public IEnumerable<Document> Documents {get; set;} = new List<Document>();
    }
}