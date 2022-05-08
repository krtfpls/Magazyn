using Application.DocumentBuilders;
using Entities.Entities.Documents;

namespace Application.Repositories
{
    public interface IDocumentRepository
    {
         IQueryable<Document>? Documents { get; }
        void SaveDocument(Document d);
        void CreateDocument(Document d, DocumentBuilder builder);
    }
}