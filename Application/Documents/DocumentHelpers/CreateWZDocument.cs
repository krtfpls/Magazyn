namespace Application.Documents.DocumentHelpers;

public sealed class CreateWZDocument: CreateOutgoingDocument
{
    public CreateWZDocument(DocumentDto document)
    {
        this.newDocument= document;
        this.newDocument.Type= "WZ";
    }

}

