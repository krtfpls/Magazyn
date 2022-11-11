using Application.Customers;

namespace Application.Documents;

public class DocumentsToReturn
{
        public Guid Id { get; set; }
        public string Type { get; set; }= string.Empty;
        public CustomerShortDto? Customer { get; set; }
        public string Number { get; set; }= string.Empty;
        public DateTime Date { get; set; } 
}
