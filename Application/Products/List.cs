using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;

namespace Application.Products;

public class List
{
    public class Query: IRequest<Result<PagedList<ProductDto>>> {
            public ProductParams Params { get; set; }   
        }
    
    public class Handler : IRequestHandler<Query, Result<PagedList<ProductDto>>>
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

          public async Task<Result<PagedList<ProductDto>>> Handle(Query request, CancellationToken cancellationToken)
            {                 
                    var query = _context.Products
                    .OrderBy(d => d.Name)
                    // .Include(u => u.User)
                    //    .Where(u => u.User.UserName == _userAccessor.GetUsername())
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                if (request.Params.CategoryName != null){
                    query = query.Where(c => c.CategoryName== request.Params.CategoryName);
                }
                 
                 return Result<PagedList<ProductDto>>.Success(
                     await PagedList<ProductDto>.CreateAysnc(query, request.Params.PageNumber, request.Params.PageSize)
                 );
            }
        }  
}
