namespace Application.Documents.DocumentBuilders;

public abstract class CreateIncomeDocument : NewDocument
{
    
    public override void AddProductLines(IEnumerable<DocumentLineDto> documentLines)
    {
      var mangleLines = new List<DocumentLineDto>();
        foreach (var line in documentLines)
          {
            line.Product.Quantity += line.Quantity;
            mangleLines.Add(line);
            }
        this.lines= mangleLines;
    }
}
