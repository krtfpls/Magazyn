using Entities;
using Entities.Documents;

namespace Application.Documents.DocumentHelpers;

public abstract class CreateOutgoingDocument : NewDocument
{
  private DocumentLine _line;

  public override DocumentLine UpdateProductLine(Product product, int qty)
    {
            product.Quantity-=qty;
            
            _line= new DocumentLine{
                Product = product,
                Quantity= qty
            };
            
      return _line;
    }
}
