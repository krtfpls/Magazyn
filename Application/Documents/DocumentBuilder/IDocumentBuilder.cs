using Application.Documents.DocumentHelpers;
using Entities;
using Entities.Documents;

namespace Application.Documents.DocumentBuilder
{
    public interface IDocumentBuilder
    {
        string GetType();
        void SetType (int type);
        void SetNumber(string number);
        void SetCustomer(int customerId);
        void SetDate(DateOnly date);
        void SetUser(string userId);
        void AddLine(Product product, int qty);
        Document? Build();
    }
}