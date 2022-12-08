using Application.Core;

namespace Application.Documents;

public class DocumentParams: PagingParams
{
    public string? Type { get; set; }
    public DateOnly? DateFrom {get; set;}
    public DateOnly? DateTo {get; set;}
}
