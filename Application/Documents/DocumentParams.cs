using Application.Core;

namespace Application.Documents;

public class DocumentParams: PagingParams
{
    public string? Type { get; set; }
    public DateTime? DateFrom {get; set;}
    public DateTime? DateTo {get; set;}
}
