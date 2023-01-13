using Application.Categories;
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
        CreateProjection<Document, DocumentsToReturn>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type!.Name));
        CreateProjection<Document, DocumentDetails>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Name));
        CreateProjection<Document, DocumentDto>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Name));;
        CreateProjection<DocumentLine, DocumentLineDetails>();
        CreateProjection<DocumentLine, DocumentLineDto>();//.ReverseMap();
        CreateProjection<Product, ProductDto>();
        CreateProjection<ProductsShortDto, Product>();
        CreateProjection<Customer, CustomerShortDto>();
        CreateProjection<Customer, CustomerDto>();//.ReverseMap();
        CreateProjection<Product, ProductsShortDto>();
        CreateProjection<Product, ProductLine>();
        
        CreateMap<Category, CategoryDto>();
        CreateMap<ProductDto, Product>();
        
    }
}
