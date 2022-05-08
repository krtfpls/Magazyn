using Application.Products;
using AutoMapper;
using Entities.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductsListDto>();
   
    }
}
