using Application.Core;
using AutoMapper;
using Data;
using Entities.Documents;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers;

public class Edit
{
    public class Command : IRequest<Result<Unit>>{
           public CustomerDto Customer {get;set;} = new CustomerDto();
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
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            { 
                var _customer = await _context.Customers.FirstAsync(s => s.Id == request.Customer.Id);
                
                _mapper.Map(request.Customer, _customer);

                    var result= await _context.SaveChangesAsync() > 0 ;

                     if (!result) return Result<Unit>.Failure("Failed to update Customer");

                    return Result<Unit>.Success(Unit.Value);
            }
        }
}
