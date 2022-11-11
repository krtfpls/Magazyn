using Entities.Documents;

namespace Application.Documents.DocumentHelpers;

public sealed class CreatePZDocument: CreateIncomingDocument
{
    public CreatePZDocument(DocumentDto document)
    {
        this.newDocument= document;
        this.newDocument.Type= "PZ";
    }
}
