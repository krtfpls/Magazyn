using Application.Core;
using Data;
using MediatR;

namespace Application.Customers;

public class Delete
{
     public class Command: IRequest<Result<Unit>>{
            public int Id {get;set;}
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var CustomerCount = _context.Documents
                                .Where(c => c.CustomerId == request.Id)
                                .Count();

                if (CustomerCount > 0)
                    return Result<Unit>.Failure("Customer has documents");
                
                var item = await _context.Customers.FindAsync(request.Id);
             
                if (item != null)
                    _context.Customers.Remove(item);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to remove Customer");
                return Result<Unit>.Success(Unit.Value);
            }
        }
}
