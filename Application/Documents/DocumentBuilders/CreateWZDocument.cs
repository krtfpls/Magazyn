namespace Application.Documents.DocumentBuilders;

public sealed class CreateWZDocument: CreateOutcomeDocument
{
    public CreateWZDocument(DocumentDto document)
    {
        SetType("WZ");
        SetCustomer(document.Customer.Id);
        SetDate(document.Date);
        SetNumber(document.Number);
    }

}
