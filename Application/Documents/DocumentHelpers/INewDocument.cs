using Entities;
using Entities.Documents;

namespace Application.Documents.DocumentHelpers;

public interface INewDocument
{
   public DocumentLine UpdateProductLine(Product product, int qty);
}
