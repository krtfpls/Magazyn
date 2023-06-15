namespace Application.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }= string.Empty;

        public string SerialNumber { get; set; } = string.Empty;

        public decimal PriceNetto { get; set; } = 0.01m;
        public int? MinLimit { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set;}
    }
}