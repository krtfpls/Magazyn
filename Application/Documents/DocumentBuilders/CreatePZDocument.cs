using Entities.Entities.Documents;

namespace Application.Documents.DocumentBuilders;

public sealed class CreatePZDocument: CreateIncomeDocument
{
    public CreatePZDocument(DocumentDto document)
    {
        SetType("PZ");
        SetCustomer(document.Customer.Id);
        SetDate(document.Date);
        SetNumber(document.Number);
    }

}
