using Entities;
using Entities.Documents;
using Microsoft.AspNetCore.Identity;

namespace Data;

public class Seed
{
    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {

        var _categories = setCategories();
        var _customers = setCustomer();
        var _document = new Document();
        var _documentTypes = setDocTypes();
        List<Product> _productList1 = setProductList1(context);
        List<Product> _productList2 = setProductList2(context);
        List<Product> _productList3 = setProductList3(context);

        if (!context.Users.Any())
        {
            var _user = setUser();
            userManager.CreateAsync(_user, "P@ssword1");
        }

        if (!context.DocumentTypes.Any())
        {
            await context.DocumentTypes.AddRangeAsync(_documentTypes);
            context.SaveChanges();
        }

        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(_categories);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {

            await context.Customers.AddRangeAsync(_customers);
            context.SaveChanges();
        }

        // if (!System.Diagnostics.Debugger.IsAttached)
        // {
        if (!context.Products.Any())
        {
            var _user = context.Users.SingleOrDefault(x => x.UserName == "admin");

            foreach (var item in _productList1)
            {
                item.User = _user;
            }
            foreach (var item in _productList2)
            {
                item.User = _user;
            }
            foreach (var item in _productList3)
            {
                item.User = _user;
            }

            await context.Products.AddRangeAsync(_productList1);
            await context.Products.AddRangeAsync(_productList2);
            await context.Products.AddRangeAsync(_productList3);

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

    // private methods

    private static List<Product> setProductList1(DataContext context)
    {
        return new List<Product>{
                  new Product
                  {
                      Name = "Posnet Thermal HD Online",
                      SerialNumber = "123456789",
                      PriceNetto = 2300,
                      MinLimit = 2,
                      Description = "drukarka",
                      Category = context.Categories.SingleOrDefault(x => x.Name == "Drukarka Fiskalna".ToLower())
                  },
                   new Product
                 {
                     Name = "Elzab Mera",
                     SerialNumber = "474202873",
                     PriceNetto = 2399,
                     MinLimit = 1,
                     Description = "drukarka testowa",
                     Category = context.Categories.SingleOrDefault(x => x.Name == "Drukarka Fiskalna".ToLower())
                 },
                 new Product
                {
                    Name = "4GB DDR4",
                    SerialNumber = "",
                    PriceNetto = 100,
                    MinLimit = 1,
                    Description = "pamięć testowa",
                    Category = context.Categories.SingleOrDefault(x => x.Name == "Pamięć RAM".ToLower())
                }
            };
    }


    private static List<Product> setProductList2(DataContext context)
    {
        return new List<Product>{

                        new Product
                            {
                                Name = "zasilacz Elzab P10",
                                SerialNumber = "brak",
                                PriceNetto = 109,
                                MinLimit = 1,
                                Description = "zasilacz testowy",
                                Category = context.Categories.SingleOrDefault(x => x.Name ==  "Zasilacz".ToLower())
                            },
                           new Product
                    {
                        Name = "patchcord 3m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.SingleOrDefault(x => x.Name == "Patchcord".ToLower())
                    },
                    new Product
                    {
                        Name = "patchcord 5m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.SingleOrDefault(x => x.Name == "Patchcord".ToLower())
                    },
                    new Product
                    {
                        Name = "patchcord 1m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.SingleOrDefault(x => x.Name == "Patchcord".ToLower())
                    },
                    new Product
                    {
                        Name = "patchcord 0,5m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.SingleOrDefault(x => x.Name == "Patchcord".ToLower())
                    }
            };
    }
    
    private static List<Product> setProductList3(DataContext context)
    {
        return new List<Product>
            {
                new Product
                            {
                                Name = "zasilacz Elzab P10",
                                SerialNumber = "brak",
                                PriceNetto = 109,
                                MinLimit = 1,
                                Description = "zasilacz testowy",
                                Category = context.Categories.SingleOrDefault(x => x.Name ==  "Zasilacz".ToLower())
                            }, new Product
                            {
                                Name = "gniazdo pojedyncze",
                                SerialNumber = "",
                                PriceNetto = 120,
                                MinLimit = 20,
                                Description = "gniazdo testowe",
                                Category = context.Categories.SingleOrDefault(x => x.Name == "Gniazdo RJ45".ToLower())
                            },
                             new Product
                            {
                                Name = "gniazdo podwojne",
                                SerialNumber = "",
                                PriceNetto = 20,
                                MinLimit = 20,
                                Description = "gniazdo testowe",
                                Category = context.Categories.SingleOrDefault(x => x.Name == "Gniazdo RJ45".ToLower())
                            },new Product
                            {
                                Name = "patchcord 2m",
                                SerialNumber = "",
                                PriceNetto = 5,
                                MinLimit = 20,
                                Description = "patchcord testowy",
                                Category = context.Categories.SingleOrDefault(x => x.Name == "Patchcord".ToLower())
                             }, new Product
                            {
                                Name = "patchcord 0,25m",
                                SerialNumber = "",
                                PriceNetto = 5,
                                MinLimit = 20,
                                Description = "patchcord testowy",
                                Category = context.Categories.SingleOrDefault(x => x.Name == "Patchcord".ToLower())
                            }

         };
    }

    private static IEnumerable<Category> setCategories()
    {
        return new List<Category>{
                    //0
                    new Category{
                        Name="Drukarka Fiskalna".ToLower()
                    },
                    //1
                    new Category{
                        Name="Dysk SSD".ToLower()
                    },
                    //2
                    new Category{
                        Name="POS".ToLower()
                    },
                    //3
                    new Category{
                        Name="Przewód USB".ToLower()
                    },
                    //4
                    new Category{
                        Name="Pamięć RAM".ToLower()
                    },
                    //5
                    new Category{
                        Name="Switch".ToLower()
                    },
                    //6
                    new Category{
                        Name="Szafa Rack".ToLower()
                    },
                    //7
                    new Category{
                        Name="Zasilacz".ToLower()
                    },
                    //8
                    new Category{
                        Name="Karta SD".ToLower()
                    },
                    //9
                    new Category{
                        Name="Mechanizm Drukujący".ToLower()
                    },
                    //10
                    new Category{
                        Name="Pendrive".ToLower()
                    },
                    //11
                    new Category{
                        Name="Zestaw Serwisowy".ToLower()
                    },
                    //12
                    new Category{
                        Name="Patchcord".ToLower()
                    },
                    //13
                    new Category{
                        Name="Gniazdo RJ45".ToLower()
                    }
                };
    }

    private static IEnumerable<DocumentType> setDocTypes()
    {
        return new List<DocumentType>{
            new DocumentType{
                Name = "PZ",
            },
            new DocumentType{
                Name = "WZ",
            },
              new DocumentType{
                Name = "GoodsReceiptNote",
            },
              new DocumentType{
                Name = "GoodsDispachedNote",
            }
            };
    }

    private static Customer setCustomer()
    {
        return new Customer
        {
            Name = "Eurocash Krosno",
            Description = "Centrala",
            Street = "Przemysłowa",
            StreetNumber = "34",
            City = "Chorkówka",
            TaxNumber = "7791906082"
        };
    }

    private static User setUser()
    {
        var user = new User
        {
            UserName = "admin",
            FirstName= "admin",
            LastName= "admin",
            Email = "admin@test.pl"
        };

        return user;
    }
}
