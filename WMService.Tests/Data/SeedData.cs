namespace WMService.Tests.Data
{
    public class SeedData
    {
         if (!System.Diagnostics.Debugger.IsAttached)
        {
        if (!context.Products.Any())
        {
            var _user = context.Users.SingleOrDefault(x => x.UserName == "admin");

            var productsData1 = await File.ReadAllTextAsync("seed_JSONs/productList1.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var productList1 = JsonSerializer.Deserialize<List<Product>>(productsData1);
            if (productList1 != null)
                {
            foreach (var item in productList1)
            {
                item.UserId = _user.Id;
                CategoryHandle category= new CategoryHandle(requestProduct.CategoryName, _context);
                item.Category= CategoryHandle()
            }
             await context.Products.AddRangeAsync(productList1);
            }

            var productsData2 = await File.ReadAllTextAsync("seed_JSONs/productList2.json");
            var productList2 = JsonSerializer.Deserialize<List<Product>>(productsData2);
            if (productList2 != null)
                {
            foreach (var item in productList2)
            {
                item.UserId = _user.Id;
            }
             await context.Products.AddRangeAsync(productList2);
            }

            var productsData3 = await File.ReadAllTextAsync("seed_JSONs/productList3.json");
            var productList3 = JsonSerializer.Deserialize<List<Product>>(productsData3);
            if (productList3 != null)
                {
            foreach (var item in productList3)
            {
                item.UserId = _user.Id;
            }
             await context.Products.AddRangeAsync(productList3);
            }

            context.SaveChanges();
        };


        if (!context.Documents.Any())
        {
            _document.Customer = context.Customers.SingleOrDefault(x => x.TaxNumber == "7791906082");
            _document.Date = DateOnly.FromDateTime(DateTime.Now);
            _document.Number = "1";
            _document.Type = context.DocumentTypes.SingleOrDefault(x => x.Name == "PZ");
            _document.User = context.Users.SingleOrDefault(x => x.UserName == "admin");

            List<DocumentLine> docLines = new List<DocumentLine>();

            foreach (var line in _productList1)
            {
                int qty = 1;
                Product _product = context.Products.SingleOrDefault(x => x.Name == line.Name);
                if (_product != null){
                    _product.Quantity = qty;
                }
                docLines.Add(new DocumentLine
                {
                    Product = line,
                    Quantity = qty
                });
            }

            _document.DocumentLines = docLines;

            await context.Documents.AddAsync(_document);
            context.SaveChanges();
        }
    }
    }
}