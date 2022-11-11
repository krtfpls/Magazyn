using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class Details
{
    public class Query : IRequest<Result<ProductDto>>
    {
        public Guid id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ProductDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
       // private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IMapper mapper )//, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            //_userAccessor = userAccessor;
        }

        public async Task<Result<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var item = await _context.Products
           // .Include(u => u.User)
           // .Where(u => u.User.UserName == _userAccessor.GetUsername())
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.id);

        if (item != null)
            return Result<ProductDto>.Success(item);
        else
            return Result<ProductDto>.Failure("Can't find that product");
        }
    }
}
