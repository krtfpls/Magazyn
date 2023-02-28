using Application.Core;
using AutoMapper;
using Data;
using Entities.Documents;
using FluentValidation;
using MediatR;

namespace Application.Customers;

public class Create
{
    public class Command : IRequest<Result<int>>
    {
        public CustomerDto Customer { get; set; } = new CustomerDto();
    }

    public class CommandValidator : AbstractValidator<Command>
    {

        public CommandValidator()
        {
            RuleFor(x => x.Customer).SetValidator(new CustomersValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<int>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            _context = context;
        }

        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            Customer newCustomer = _mapper.Map<Customer>(request.Customer);
                newCustomer.Id=0;
                
            _context.Customers.Add(newCustomer);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<int>.Failure("Cannot to create new Customer");
            return Result<int>.Success(newCustomer.Id);
        }
    }
}
