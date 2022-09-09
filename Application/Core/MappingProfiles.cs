using Application.Customers;
using Application.Documents;
using Application.Products;
using AutoMapper;
using Entities;
using Entities.Documents;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Document, DocumentToReturn>().ReverseMap();
        CreateMap<DocumentLine, DocumentLineDto>().ReverseMap();
        CreateMap<Document, DocumentDto>();



        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductsShortDto>();
        
        CreateMap<Customer, CustomerShortDto>();
        CreateMap<Customer, CustomerDto>().ReverseMap();
   
    }
}
