using Application.Core;
using AutoMapper;
using Data;
using Entities.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class Edit
{
    public class Command : IRequest<Result<Unit>>{
           public ProductDto Product {get;set;}
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
              private readonly IMapper _mapper;
            //private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper)//, IUserAccessor userAccessor)           
            {
                _context = context;
                _mapper = mapper;
                //_userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            { 
                var requestProduct = request.Product;

                var product = await _context.Products
                            .Include(c => c.Category)
                            .FirstOrDefaultAsync(x => x.Id == requestProduct.Id);
                            
                if (product== null) return null;
                
                var requestCategory = requestProduct.CategoryName.Trim();

                if (product.Category.Name != requestCategory){
                    var category = await _context.Categories
                                        .FirstOrDefaultAsync(x => x.Name == requestCategory);

                    product.Category = category ?? new Category{Name = requestCategory};
                                            
                }

                _mapper.Map(requestProduct, product);
             
                    var result= await _context.SaveChangesAsync() > 0 ;

                     if (!result) return Result<Unit>.Failure("Failed to update Product");

                    return Result<Unit>.Success(Unit.Value);
            }
        }
}
