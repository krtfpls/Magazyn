using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers;

public class List
{
    public class Query : IRequest<Result<PagedList<CustomerShortDto>>>
    {
        public CustomerParams Params { get; set; } = new CustomerParams(); 
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<CustomerShortDto>>>
    {
        DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CustomerShortDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var customerList =  _context.Customers
                .AsNoTracking()
                .ProjectTo<CustomerShortDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return Result<PagedList<CustomerShortDto>>.Success(
                await PagedList<CustomerShortDto>.CreateAysnc(customerList, request.Params.PageNumber, request.Params.PageSize));

        }
    }
}
