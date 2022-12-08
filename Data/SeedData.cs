using Entities;
using Entities.Documents;
using Microsoft.AspNetCore.Identity;

namespace Data;

public class Seed
{
    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        
        var _categories = new List<Category>();
        var _customers = setCustomer();
        var _user = setUser();
        var _document = new Document();
        var _documentTypes = setDocTypes();
        List<Product> _productList1= setProductList1(context);
        List<Product> _productList2 = setProductList2(context);
        List<Product> _productList3 = setProductList3(context);

        if (!context.Users.Any())
        {
            await userManager.CreateAsync(_user, "P@ssword1");
        }

        if (!context.DocumentTypes.Any())
        {
            await context.DocumentTypes.AddRangeAsync(_documentTypes);
            await context.SaveChangesAsync();
        }

        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(setCategories());
            await context.SaveChangesAsync();
        }

        if (!context.Customers.Any())
        {
           // _customers.Add(setCustomer());

            await context.Customers.AddRangeAsync(_customers);
            await context.SaveChangesAsync();
        }

        if (!System.Diagnostics.Debugger.IsAttached)
        {
            if (!context.Products.Any())
            {
             //   _productList1 = setProductList1(context);


               // _productList2 = setProductList2(context);


               // _productList3 = setProductList3(context);

            

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

            await context.SaveChangesAsync();
            };
       

        if (!context.Documents.Any()){
            _document.Customer= _customers;
            _document.Date= DateOnly.FromDateTime(DateTime.Now);
            _document.Number ="1";
            _document.Type= _documentTypes.FirstOrDefault(x => x.Name == "PZ");
            _document.User = _user;

            List<DocumentLine> docLines = new List<DocumentLine>();
            
            foreach (var line in _productList1){
                docLines.Add(new DocumentLine{
                    Product = line,
                    Quantity = 1
                });
            }

            await context.Documents.AddAsync(_document);
            await context.SaveChangesAsync();
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
                                Category = context.Categories.FirstOrDefault(x => x.Name ==  "Zasilacz")
                            },
                           new Product
                    {
                        Name = "patchcord 3m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.FirstOrDefault(x => x.Name == "Patchcord")
                    },
                    new Product
                    {
                        Name = "patchcord 5m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.FirstOrDefault(x => x.Name == "Patchcord")
                    },
                    new Product
                    {
                        Name = "patchcord 1m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.FirstOrDefault(x => x.Name == "Patchcord")
                    },
                    new Product
                    {
                        Name = "patchcord 0,5m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.FirstOrDefault(x => x.Name == "Patchcord")
                    }
            };
        }
        
        

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
                      Category = context.Categories.FirstOrDefault(x => x.Name == "Drukarka Fiskalna")
                  },
                   new Product
                 {
                     Name = "Elzab Mera",
                     SerialNumber = "474202873",
                     PriceNetto = 2399,
                     MinLimit = 1,
                     Description = "drukarka testowa",
                     Category = context.Categories.FirstOrDefault(x => x.Name == "Drukarka Fiskalna")
                 },
                 new Product
                {
                    Name = "4GB DDR4",
                    SerialNumber = "",
                    PriceNetto = 100,
                    MinLimit = 1,
                    Description = "pamięć testowa",
                    Category = context.Categories.FirstOrDefault(x => x.Name == "Pamięć RAM")
                }
            };
        }

        private static IEnumerable<Category> setCategories()
        {
            return new List<Category>{
                    //0
                    new Category{
                        Name="Drukarka Fiskalna"
                    },
                    //1
                    new Category{
                        Name="Dysk SSD"
                    },
                    //2
                    new Category{
                        Name="POS"
                    },
                    //3
                    new Category{
                        Name="Przewód USB"
                    },
                    //4
                    new Category{
                        Name="Pamięć RAM"
                    },
                    //5
                    new Category{
                        Name="Switch"
                    },
                    //6
                    new Category{
                        Name="Szafa Rack"
                    },
                    //7
                    new Category{
                        Name="Zasilacz"
                    },
                    //8
                    new Category{
                        Name="Karta SD"
                    },
                    //9
                    new Category{
                        Name="Mechanizm Drukujący"
                    },
                    //10
                    new Category{
                        Name="Pendrive"
                    },
                    //11
                    new Category{
                        Name="Zestaw Serwisowy"
                    },
                    //12
                    new Category{
                        Name="Patchcord"
                    },
                    //13
                    new Category{
                        Name="Gniazdo RJ45"
                    }
                };
        }

        private static IEnumerable<DocumentType> setDocTypes()
        {
            return new List<DocumentType>{
            new DocumentType{
                Name = "PZ",
                isIncomeType=true
            },
            new DocumentType{
                Name = "WZ",
                isIncomeType=false
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
                Email = "admin@test.pl"
            };

            return user;
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
                                Category = context.Categories.FirstOrDefault(x => x.Name ==  "Zasilacz")
                            }, new Product
                            {
                                Name = "gniazdo pojedyncze",
                                SerialNumber = "",
                                PriceNetto = 120,
                                MinLimit = 20,
                                Description = "gniazdo testowe",
                                Category = context.Categories.FirstOrDefault(x => x.Name == "Gniazdo RJ45")
                            },
                             new Product
                            {
                                Name = "gniazdo podwojne",
                                SerialNumber = "",
                                PriceNetto = 20,
                                MinLimit = 20,
                                Description = "gniazdo testowe",
                                Category = context.Categories.FirstOrDefault(x => x.Name == "Gniazdo RJ45")
                            },new Product
                            {
                                Name = "patchcord 2m",
                                SerialNumber = "",
                                PriceNetto = 5,
                                MinLimit = 20,
                                Description = "patchcord testowy",
                                Category = context.Categories.FirstOrDefault(x => x.Name == "Patchcord")
                             }, new Product
                            {
                                Name = "patchcord 0,25m",
                                SerialNumber = "",
                                PriceNetto = 5,
                                MinLimit = 20,
                                Description = "patchcord testowy",
                                Category = context.Categories.FirstOrDefault(x => x.Name == "Patchcord")
                            },new Product
                    {
                        Name = "patchcord 0,5m",
                        SerialNumber = "",
                        PriceNetto = 5,
                        MinLimit = 20,
                        Description = "patchcord testowy",
                        Category = context.Categories.FirstOrDefault(x => x.Name == "Patchcord")
                    },
         };
    }
}
