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
        public class Query : IRequest<Result<PagedList<CategoryDto>>>
    {
        public CategoryParams Params { get; set; } = new CategoryParams(); 
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<CategoryDto>>>
    {
        DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CategoryDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = _context.Categories
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return Result<PagedList<CategoryDto>>.Success(
                await PagedList<CategoryDto>.CreateAysnc(categories, request.Params.PageNumber, request.Params.PageSize)
                );
        }
    }
    }
}