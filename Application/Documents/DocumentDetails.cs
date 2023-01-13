using Application.Customers;

namespace Application.Documents
{
    public class DocumentDetails
    {
        public Guid Id { get; set; }
        public string Type { get; set; }= string.Empty;
        public CustomerShortDto? Customer { get; set; }
        public string Number { get; set; }= string.Empty;
        public DateOnly Date { get; set; } 
        public List<DocumentLineDetails> DocumentLines { get; set; }
    }
}