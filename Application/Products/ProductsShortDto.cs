namespace Application.Products;

public class ProductsShortDto
{
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public decimal PriceNetto { get; set; }
        public string CategoryName { get; set;}
        public int Quantity {get; set;}
}
