using Application.Core;

namespace Application.Documents;

public class DocumentParams: PagingParams
{
    public string? Type { get; set; }
}
