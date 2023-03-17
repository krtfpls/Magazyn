using Entities;
using Entities.Documents;

namespace Application.Documents.DocumentHelpers;

public abstract class CreateInboundDocument : NewDocument
{
    private DocumentLine _line;

    public override DocumentLine UpdateProductLine(Product product, int qty)
    {
        // check if it's unique product
        if (product.SerialNumber?.Length > 0)
        {
            if (product.Quantity==-1 || product.Quantity==0)
                qty = 1;
            else
                qty = 0;
        }

        product.Quantity += qty;

        _line = new DocumentLine()
        {
            Product = product,
            Quantity = qty
        };

        return _line;
    }
}
