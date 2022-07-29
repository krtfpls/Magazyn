using Application.Core;
using AutoMapper;
using Data;
using Entities.Entities;
using FluentValidation;
using MediatR;

namespace Application.Documents;

public class Create
{
     public class Command : IRequest<Result<Unit>>
    {
        public DocumentDto Document { get; set; }
    }

     public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Document).SetValidator(new DocumentsValidator());
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
     //   private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)//, IUserAccessor userAccessor)
        {
            _mapper = mapper;
            _context = context;
          //  _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            
            var products = new List<Product>();

                foreach 

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
                return Result<Unit>.Failure("Failed to create new Document");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

}
