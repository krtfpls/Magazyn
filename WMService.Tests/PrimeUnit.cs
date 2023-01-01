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
        var c = new CustomerDto{
                    Name= "test name", 
                    Street="test street", 
                    StreetNumber="test serial", 
                    City= "test city"
                    };
        
        //Act
        c.Name = "New Name";

        //Assert
        Assert.Equal("New Name", c.Name);
    }

    [Fact]
    public void CanCreatePZDokument(){

        List<Product> productList1 = setProductList1();
        DocumentDto documentDto = setDocumentDto(productList1);

        CreatePZDocument newDok = new CreatePZDocument(documentDto);
        var productLines = new List<DocumentLine>();

        //Arrange
        foreach (var lines in newDok.newDocument.DocumentLines){
            Product producttmp = productList1.FirstOrDefault(x => x.Id == lines.ProductId);
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
           // Assert.Equal(productLines.FirstOrDefault(x => x.Product.Id== productDto.ProductId).Product.Quantity, productList1.FirstOrDefault(n => n.Id== productDto.ProductId).Quantity +productDto.Quantity);
            Assert.Equal(productLines.FirstOrDefault(x => x.Product.Id== productDto.ProductId).Quantity, productDto.Quantity);
        }
    }

     [Fact]
    public void CanCreateWZDokument(){

        List<Product> productList1 = setProductList1();
        DocumentDto documentDto = setDocumentDto(productList1);
        CreateWZDocument newDok = new CreateWZDocument(documentDto);
        var productLines = new List<DocumentLine>();

        //Arrange
        foreach (var lines in newDok.newDocument.DocumentLines){
            Product producttmp = productList1.FirstOrDefault(name => name.Id == lines.ProductId);
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
            //  Assert.Equal(productLines.FirstOrDefault(x => x.Product.Id== productDto.ProductId).Product.Quantity, productList1.FirstOrDefault(n => n.Id== productDto.ProductId).Quantity -productDto.Quantity);
          
            // Assert.Equal(productLines.FirstOrDefault(x => x.Product.Id== productDto.ProductId).Product.Quantity, productList1.FirstOrDefault(n => n.Id== productDto.ProductId).Quantity - productDto.Quantity);
             Assert.Equal(productLines.FirstOrDefault(x => x.Product.Id== productDto.ProductId).Quantity, productDto.Quantity);
        }
    }


    private DocumentDto setDocumentDto(List<Product> productList)
    {
        return new DocumentDto{
        Number= "1/test",
        CustomerId= 1,
        Date=DateOnly.FromDateTime(DateTime.Now),
        Type= "Test",
        DocumentLines= setDocumentLineDto(productList)
    };
    }

    private IEnumerable<DocumentLineDto> setDocumentLineDto(List<Product> productList)
    {
        return new List<DocumentLineDto>{
            new DocumentLineDto{
                ProductId= productList[0].Id,
                Quantity = 5
                  },
            new DocumentLineDto{
                ProductId= productList[1].Id,
                 Quantity= 0
            },
            new DocumentLineDto{
                ProductId =  productList[2].Id,
                Quantity = -6
            }
            };
    }

    private List<Product> setProductList1()
    {
        return new List<Product>{
                  new Product
                  {
                    Id= Guid.NewGuid(),
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
                    Id= Guid.NewGuid(),
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
                    Id= Guid.NewGuid(),
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
