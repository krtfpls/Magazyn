using Application.Core;
using AutoMapper;
using Data;
using Entities.Documents;
using FluentValidation;
using MediatR;

namespace Application.Customers;

public class Create
{
    public class Command : IRequest<Result<Unit>>
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

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {

            _context.Customers.Add(_mapper.Map<Customer>(request.Customer));

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Cannot to create new Customer");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
