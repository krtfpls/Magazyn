using Entities;
using Entities.Documents;

namespace Application.Documents.DocumentHelpers;

public abstract class NewDocument:INewDocument
{
    public DocumentDto newDocument {get; protected set;}
    public abstract DocumentLine UpdateProductLine(Product product, int qty);
}
