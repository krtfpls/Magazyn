using Application.Customers;

namespace Application.Documents;

public class DocumentToReturn
{
        public Guid Id { get; set; }
        public string Type { get; set; }
        public CustomerShortDto Customer { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; } 
}
