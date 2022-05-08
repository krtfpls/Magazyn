namespace Application.Products;

public class ProductsListDto
{
     public Guid Id { get; set; }

        public string Name { get; set; }

        public string? SerialNumber { get; set; }

        public int Quantity { get; set; }

        public string CategoryName { get; set; }
}
