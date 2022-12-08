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
        // var config = new MapperConfiguration(cfg =>
        // {
        //     cfg.AddCollectionMappers();
        //     // cfg.CreateProjection<Document, DocumentDetails>()
        //     //     .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Name));
        //     // cfg.CreateProjection<DocumentLine, DocumentLineDetails>();
        //     // cfg.CreateProjection<List<DocumentLine>, List<DocumentLineDetails>>();
        // });

        // CreateMap<Document, DocumentsToReturn>()
        //     .ForMember(d => d.Type, o => o.MapFrom(s => s.Type!.Name));

        CreateProjection<Document, DocumentsToReturn>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type!.Name));
        CreateProjection<Document, DocumentDetails>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Name));
        CreateProjection<DocumentLine, DocumentLineDetails>();
        CreateProjection<DocumentLine, DocumentLineDto>();//.ReverseMap();
        CreateProjection<Document, DocumentDto>();
        CreateProjection<Product, ProductDto>();//.ReverseMap();
        CreateProjection<Product, ProductsShortDto>();
        CreateProjection<Product, ProductLine>();
        

        CreateProjection<Customer, CustomerShortDto>();
        CreateProjection<Customer, CustomerDto>();//.ReverseMap();

    }
}
