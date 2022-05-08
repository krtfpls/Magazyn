using Application.Core;
using AutoMapper;
using Data;
using Entities.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public ProductDto Product { get; set; }

    }

    // public class CommandValidator : AbstractValidator<Command>
    // {
    //     public CommandValidator()
    //     {
    //         RuleFor(x => x.Item).SetValidator(new ProductValidator());
    //     }
    // }
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
            var requestProduct = request.Product;
            var requestCategory = requestProduct.CategoryName.Trim();

            var category = await _context.Categories
                        .FirstOrDefaultAsync(x =>
                            x.Name == requestCategory) ??
                    new Category() { Name = requestCategory };

            Product newProduct = _mapper.Map<Product>(requestProduct);

            newProduct.Id = default;
            newProduct.Quantity = default;
            newProduct.Category= category;

            _context.Products.Add(newProduct);

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
                return Result<Unit>.Failure("Failed to create new Product");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
