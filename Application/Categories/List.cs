using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories
{
    public class List
    {
        public class Query : IRequest<Result<List<CategoryDto>>>
    {

    }

    public class Handler : IRequestHandler<Query, Result<List<CategoryDto>>>
    {
        DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<CategoryDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .Take(100)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<List<CategoryDto>>.Success(categories);

        }
    }
    }
}