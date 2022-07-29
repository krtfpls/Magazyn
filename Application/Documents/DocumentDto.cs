using Application.Customers;

namespace Application.Documents;

public class DocumentDto
{
        public Guid Id { get; set; }
        public string Type { get; set; }
        public CustomerShortDto Customer { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; } 
        // Lines
        public IEnumerable<DocumentLineDto> DocumentLines { get; set; } = new List<DocumentLineDto>();
}
