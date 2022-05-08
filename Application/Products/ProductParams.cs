using Application.Core;

namespace Application.Products;

public class ProductParams : PagingParams
{
    public string? CategoryName { get; set; }
}
