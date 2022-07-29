using Application.Products;

namespace Application.Documents;

public class DocumentLineDto
{
        public int Id {get;set;}
        public ProductsShortDto Product { get; set; }
        public int Quantity { get; set; }
}
