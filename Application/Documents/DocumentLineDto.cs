using Application.Products;

namespace Application.Documents;

public class DocumentLineDto
{
        public int Id {get;set;}
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
}
