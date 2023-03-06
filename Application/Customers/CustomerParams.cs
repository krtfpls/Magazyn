using Application.Core;

namespace Application.Customers
{
    public class CustomerParams: PagingParams
    {
        public string? Name { get; set; }
    }
}