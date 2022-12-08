using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class List
{
    public class Query: IRequest<Result<PagedList<ProductsShortDto>>> {
            public ProductParams Params { get; set; } = new ProductParams(); 
        }
    
    public class Handler : IRequestHandler<Query, Result<PagedList<ProductsShortDto>>>
        {
            DataContext _context;
            private readonly IMapper _mapper;
            //private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper) //, IUserAccessor userAccessor)
            {
                _context= context;
                _mapper = mapper;
               // _userAccessor = userAccessor;
            }

          public async Task<Result<PagedList<ProductsShortDto>>> Handle(Query request, CancellationToken cancellationToken)
            {               
                    var query = _context.Products
                    .OrderBy(d => d.Name)
                     .Include(u => u.User)
                        .Where(u => u.User.UserName == "admin")
                    //    .Where(u => u.User.UserName == _userAccessor.GetUsername())
                    .ProjectTo<ProductsShortDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .AsQueryable();

                if (request.Params.CategoryName != null){
                    query = query.Where(c => c.CategoryName== request.Params.CategoryName);
                }
                 
                 return Result<PagedList<ProductsShortDto>>.Success(
                     await PagedList<ProductsShortDto>.CreateAysnc(query, request.Params.PageNumber, request.Params.PageSize)
                 );
            }
        }  
}
