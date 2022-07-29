namespace Application.Documents.DocumentBuilders;

public abstract class NewDocument : INewDocument
{
    string type;
    int customerId;
    string number;
    DateTime date;
    protected List<DocumentLineDto> lines = new List<DocumentLineDto>();

    public abstract void AddProductLines(IEnumerable<DocumentLineDto> documentLines);

    public DocumentDto Build()
    {
        if (lines == null)
            throw new Exception("Document has no lines!");
        if (number == null)
            throw new Exception("Document has no number");
        if (type == null)
            throw new Exception("Document has no type!");
        if (date == DateTime.MinValue)
            date = DateTime.Now;
        if (customerId == 0)
            throw new Exception("Document has no customer");

        DocumentDto documentToReturn = new DocumentDto();
        documentToReturn.Customer.Id = customerId;
        documentToReturn.Date = date;
        documentToReturn.DocumentLines = lines;
        documentToReturn.Number = number;
        documentToReturn.Type = type;

        return documentToReturn;
    }

    public void SetCustomer(int customerId)
    {
        this.customerId= customerId;
    }

    public void SetDate(DateTime date)
    {
        this.date= date;
    }

    public void SetNumber(string number)
    {
        this.number = number;
    }

    public void SetType(string type)
    {
        this.type= type;
    }
}
