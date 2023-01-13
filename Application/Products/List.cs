using System.Linq;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Entities.interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class List
{
    public class Query: IRequest<Result<PagedList<ProductsShortDto>>> {
            public ProductParams Params { get; set; } = new ProductParams(); 
            public bool stock {get; set;} = false;
        }
    
    public class Handler : IRequestHandler<Query, Result<PagedList<ProductsShortDto>>>
        {
            DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context= context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

          public async Task<Result<PagedList<ProductsShortDto>>> Handle(Query request, CancellationToken cancellationToken)
            {               
                    var query = _context.Products
                     .OrderBy(d => d.Name)
                     .Where(u => u.User.Id == _userAccessor.GetUserId())
                    .AsNoTracking()
                    .ProjectTo<ProductsShortDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();
                
                if (request.stock){
                    query = query.Where(q => q.Quantity != 0 );
                }

                if (request.Params.CategoryName != null){
                     string category = request.Params.CategoryName.Trim().ToLower();
                    query = query.Where(c => c.CategoryName.Contains(category));
                }
                 
                 return Result<PagedList<ProductsShortDto>>.Success(
                     await PagedList<ProductsShortDto>.CreateAysnc(query, request.Params.PageNumber, request.Params.PageSize)
                 );
            }
        }  
}
