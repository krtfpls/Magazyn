namespace Application.Documents.DocumentBuilders;

public interface INewDocument
{
    void AddProductLines(IEnumerable<DocumentLineDto> documentLines);
    void SetType(string type);
    void SetCustomer(int customerId);
    void SetNumber(string number);
    void SetDate(DateTime date);
    public DocumentDto Build();
}
