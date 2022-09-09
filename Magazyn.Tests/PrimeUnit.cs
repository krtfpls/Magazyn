using Application.Customers;
using Application.Documents;
using Application.Documents.DocumentHelpers;
using Application.Products;
using Entities;
using Entities.Documents;
using Xunit;

namespace Magazyn.Tests;

public class PrimeUnit
{
    [Fact]
    public void CanChangeName()
    {
        //Arrange
        var c = new CustomerDto{Name= "test name", 
                                Street="test street", 
                                StreetNumber="test serial", 
                                City= "test city"};
        
        //Act
        c.Name = "New Name";

        //Assert
        Assert.Equal("New Name", c.Name);
    }

    [Fact]
    public void CanCreatePZDokument(){

        DocumentDto documentDto = setDocumentDto();
        List<Product> productList1 = setProductList1();
        CreatePZDocument newDok = new CreatePZDocument(documentDto);
        var productLines = new List<DocumentLine>();

        //Arrange
        foreach (var lines in newDok.newDocument.DocumentLines){
            Product producttmp = productList1.FirstOrDefault(name => name.Name == lines.Product.Name);
                  if (producttmp == null) break;
            Product product = new Product{
                Id= producttmp.Id,
                Name= producttmp.Name,
                Quantity= producttmp.Quantity,
                Category= producttmp.Category,
                Description =producttmp.Description,
                MinLimit=producttmp.MinLimit,
                PriceNetto=producttmp.PriceNetto,
                SerialNumber=producttmp.SerialNumber
            };

             var line = newDok.UpdateProductLine(product, lines.Quantity);

                productLines.Add(line);
        }


        //Assert
        foreach (var productDto in documentDto.DocumentLines){
            Assert.Equal(productLines.FirstOrDefault(x => x.Product.Name== productDto.Product.Name).Product.Quantity, productList1.FirstOrDefault(n => n.Name== productDto.Product.Name).Quantity +productDto.Quantity);
            Assert.Equal(productLines.FirstOrDefault(x => x.Product.Name== productDto.Product.Name).Quantity, productDto.Quantity);
        }
    }

     [Fact]
    public void CanCreateWZDokument(){

        DocumentDto documentDto = setDocumentDto();
        List<Product> productList1 = setProductList1();
        CreateWZDocument newDok = new CreateWZDocument(documentDto);
        var productLines = new List<DocumentLine>();

        //Arrange
        foreach (var lines in newDok.newDocument.DocumentLines){
            Product producttmp = productList1.FirstOrDefault(name => name.Name == lines.Product.Name);
                  if (producttmp == null) break;
            Product product = new Product{
                Id= producttmp.Id,
                Name= producttmp.Name,
                Quantity= producttmp.Quantity,
                Category= producttmp.Category,
                Description =producttmp.Description,
                MinLimit=producttmp.MinLimit,
                PriceNetto=producttmp.PriceNetto,
                SerialNumber=producttmp.SerialNumber
            };
                if (product == null) break;

             var line = newDok.UpdateProductLine(product, lines.Quantity);

                productLines.Add(line);
        }


        //Assert
        foreach (var productDto in documentDto.DocumentLines){
            Assert.Equal(productLines.FirstOrDefault(x => x.Product.Name== productDto.Product.Name).Product.Quantity, productList1.FirstOrDefault(n => n.Name== productDto.Product.Name).Quantity - productDto.Quantity);
            Assert.Equal(productLines.FirstOrDefault(x => x.Product.Name== productDto.Product.Name).Quantity, productDto.Quantity);
        }
    }


    private DocumentDto setDocumentDto()
    {
        return new DocumentDto{
        Number= "1/test",
        Customer= setCustomerDto(),
        Date=DateTime.Now,
        Type= "Test",
        DocumentLines= setDocumentLineDto()
        };
    }

    private CustomerShortDto setCustomerDto()
    {
        return new CustomerShortDto
            {
                Name = "Eurocash Krosno",
                City = "Chorkówka",
            };
    }

    private IEnumerable<DocumentLineDto> setDocumentLineDto()
    {
        return new List<DocumentLineDto>{
            new DocumentLineDto{
                Product= new ProductsShortDto
                        {
                            Name = "Posnet Thermal HD Online",
                            SerialNumber = "123456789",
                            PriceNetto = 2300,
                            },
                Quantity = 5
                  },
            new DocumentLineDto{
                Product= new ProductsShortDto
                 {
                     Name = "Elzab Mera",
                     SerialNumber = "474202873",
                     PriceNetto = 2399,
                 },
                 Quantity= 0
            },
            new DocumentLineDto{
                Product =  new ProductsShortDto
                {
                    Name = "4GB DDR4",
                    SerialNumber = "",
                    PriceNetto = 100,
                },
                Quantity = -6
            }
            };
    }

    private List<Product> setProductList1()
    {
        return new List<Product>{
                  new Product
                  {
                      Name = "Posnet Thermal HD Online",
                      SerialNumber = "123456789",
                      PriceNetto = 2300,
                      MinLimit = 2,
                      Description = "drukarka",
                      Quantity= 1,
                      Category = new Category {Name= "Drukarka Fiskalna"}
                  },
                   new Product
                 {
                     Name = "Elzab Mera",
                     SerialNumber = "474202873",
                     PriceNetto = 2399,
                     MinLimit = 1,
                     Description = "drukarka testowa",
                     Quantity = 1,
                     Category = new Category {Name= "Drukarka Fiskalna"}
                 },
                 new Product
                {
                    Name = "4GB DDR4",
                    SerialNumber = "",
                    PriceNetto = 100,
                    MinLimit = 1,
                    Description = "pamięć testowa",
                    Quantity = 1,
                    Category = new Category {Name= "Pamięć RAM"}
                }
            };
    }
}
