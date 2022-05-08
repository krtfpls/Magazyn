using Application.DocumentBuilders;
using Entities.Entities;
using Entities.Entities.Documents;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class EFDocumentRepository : IDocumentRepository
    {
        private DataContext _context;
        private DocumentBuilder _builder;

        public EFDocumentRepository(DataContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Document> Documents => _context.Documents;

        public async void CreateDocument(Document d, DocumentBuilder builder)
        {
            _builder = builder;

            DocumentDirectorBuilder directorBuilder = new DocumentDirectorBuilder(_builder);

            foreach (var line in d.DocumentLines)
            {
                Product? tempProduct = await _context.Products.FirstOrDefaultAsync(x => x.Name == line.Product.Name);

                if (tempProduct != null)
                    line.Product = tempProduct;

                directorBuilder.AddDocumentLine(line);
            }

            directorBuilder.AddDate(d.Date);
            directorBuilder.AddCustomer(await GetCustomer(d.CustomerId));

            int typeId= await GetDocumentTypeByName(_builder.TypeName);
            directorBuilder.AddType(typeId);
            directorBuilder.AddNumber(await GetLastNumberDocumentTypeByName(typeId));

            _context.Add(directorBuilder.Build());
            _context.SaveChanges();
        }

        public void SaveDocument(Document d)
        {
            _context.SaveChanges();
        }

        private async Task<int> GetLastNumberDocumentTypeByName(int typeId)
        {

            int number = await _context.Documents
                    .Where(x => x.TypeId == typeId)
                    .CountAsync() + 1;

            return number;
        }

        private async Task<int> GetDocumentTypeByName(string? typeName)
        {
            var typeID = await _context.DocumentTypes.FirstOrDefaultAsync(n => n.Name == typeName);
            if (typeID == null)
            {
                typeID = new DocumentType
                {
                    Name = typeName,
                    isIncomeType = this._builder is IncomeDocumentBuilder ? true : false
                };
                await _context.SaveChangesAsync();
            }
            return typeID.Id;
        }

        private Task<int> GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

    }
}