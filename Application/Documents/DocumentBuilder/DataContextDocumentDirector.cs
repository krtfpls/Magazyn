using Data;
using Entities;
using Entities.Documents;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.DocumentBuilder
{
    public class DataContextDocumentDirector
    {

        private readonly DataContext _context;
        private readonly IDocumentBuilder _builder;
        private readonly string _userId;

        public DataContextDocumentDirector(DataContext context, IDocumentBuilder builder, string userId)
        {
            _userId = userId;
            _context = context;
            _builder = builder;
        }

        public void SetDocument(int customerId, IEnumerable<DocumentLine> lines, DateOnly date)
        {
            SetUserByID(_userId);
            setDocumentType();
            setNumber(_userId);
            setCustomer(customerId);
            setLines(lines, _userId);
            _builder.SetDate(date);
        }

        public Document? BuildDocument()
        {
            Document? doc = _builder.Build();
            return doc;
        }

                private async void setLines(IEnumerable<DocumentLine> lines, string userId)
                {
                    if (lines != null)
                    {
                        foreach (var line in lines)
                        {

                            var checkProduct = await _context.Products
                                    .Where(p => p.Id == line.ProductId)
                                    .Where(u => u.UserId == userId)
                                    .Select(p => new Product
                                    {
                                        Id = p.Id,
                                        SerialNumber = p.SerialNumber,
                                        Quantity = p.Quantity
                                    })
                                    .FirstOrDefaultAsync();

                            if (checkProduct != null)
                                {
                                    _builder.AddLine(checkProduct, line.Quantity);
                                    _context.Attach<Product>(checkProduct);
                                    _context.Entry<Product>(checkProduct).Property(p => p.Quantity).IsModified=true;
                                }

                        }
                    }
                }

                private async void setCustomer(int customerId)
                {
                    bool check = await _context.Customers
                                .AnyAsync(x => x.Id == customerId);

                    if (check)
                        _builder.SetCustomer(customerId);
                }

                private async void SetUserByID(string userId)
                {
                    var findUser = await _context.Users
                                    .AnyAsync(user => user.Id == userId);

                    if (findUser)
                        _builder.SetUser(userId);
                }

                private async void setNumber(string userId)
                {
                    if (userId != null)
                    {
                        int date = DateTime.Now.Year;

                        string number = ((await _context.Documents
                                .Where(year => year.Date.Year == date)
                                .Where(user => user.UserId == userId)
                                .CountAsync(doc => doc.Type!.Name == _builder.GetType())) + 1).ToString() + "/" + date;

                        _builder.SetNumber(number);
                    }
                }

                private async void setDocumentType()
                {
                    string type = _builder.GetType();
                    var documentType = await _context.DocumentTypes
                                        .Where(x => x.Name == type)
                                        .Select(x => x.Id)
                                        .FirstOrDefaultAsync();

                    _builder.SetType(documentType);
                }

    }
}