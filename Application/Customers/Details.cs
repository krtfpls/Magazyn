using Application.Core;
using Data;
using Entities.Entities.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers;

public class Details
{
    public class Query: IRequest<Result<Customer>>{
            public int Id {get;set;}
        }

        public class Handler : IRequestHandler<Query, Result<Customer>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Customer>> Handle(Query request, CancellationToken cancellationToken)
            {
                   return Result<Customer>.Success(await _context.Customers.SingleOrDefaultAsync(x => x.Id == request.Id));
             
                
            }
        }
}
