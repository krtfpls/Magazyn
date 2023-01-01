using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers;

public class List
{
    public class Query : IRequest<Result<List<CustomerShortDto>>>
    {

    }

    public class Handler : IRequestHandler<Query, Result<List<CustomerShortDto>>>
    {
        DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<CustomerShortDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var customerList = await _context.Customers
                .AsNoTracking()
                .ProjectTo<CustomerShortDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<List<CustomerShortDto>>.Success(customerList);

        }
    }
}
