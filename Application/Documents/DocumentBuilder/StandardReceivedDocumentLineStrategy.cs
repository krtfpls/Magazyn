using Application.Documents.DocumentHelpers;
using Entities;
using Entities.Documents;

namespace Application.Documents.DocumentBuilder
{
    public class StandardReceivedDocumentLineStrategy 
    {
        public static DocumentLine? handleLine(Product product, int qty)
        {
            // check if it's unique product
            if (product.SerialNumber?.Length > 0)
            {
                if (product.Quantity == -1 || product.Quantity == 0)
                    qty = 1;
                else
                    return null;
            }
    
            product.Quantity += qty;

            DocumentLine line = new DocumentLine()
            {
                Product = product,
                Quantity = qty
            };

            return line;
        }
    }
}