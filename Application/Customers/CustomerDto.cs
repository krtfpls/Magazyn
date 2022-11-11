namespace Application.Customers;

public class CustomerDto
{
    public int Id {get;set;}

        public string Name {get; set;} = string.Empty;

        public string Street {get;set;} = string.Empty;
        public string StreetNumber {get;set;} = string.Empty;
        public string City {get;set;} = string.Empty;
        public string TaxNumber {get;set;} = string.Empty;
        public string Description {get;set;} = string.Empty;
}
