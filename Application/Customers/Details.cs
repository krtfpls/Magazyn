using Application.Core;
using Data;
using Entities.Documents;
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
                Customer? customer = await _context.Customers.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id);

                if (customer != null)
                   return Result<Customer>.Success(customer);
                else
                    return Result<Customer>.Failure("Can't Find that customer");
            }
        }
}
