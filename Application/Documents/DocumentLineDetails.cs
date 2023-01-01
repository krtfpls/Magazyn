using Application.Products;

namespace Application.Documents
{
    public class DocumentLineDetails
    {
        
        public int Id {get;set;}
        public ProductLine Product { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductLine
    {
         public Guid Id { get; set; }

        public string Name { get; set; }= string.Empty;

        public string SerialNumber { get; set; } = string.Empty;
        public decimal PriceNetto { get; set; }
        public string CategoryName { get; set; }= string.Empty;

    }
}