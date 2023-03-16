using Application.Categories;
using Application.Core;
using AutoMapper;
using Data;
using Entities;
using Entities.interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public ProductDto Product { get; set; } = new ProductDto();
    }

        public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Product).SetValidator(new ProductsValidator());
        }
    }
    
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)           
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestProduct = request.Product;

            var product = await _context.Products
                        .Where(u => u.User.Id == _userAccessor.GetUserId())
                        .Include(c => c.Category)
                        .FirstOrDefaultAsync(x => x.Id == requestProduct.Id);

            if (product == null) return Result<Unit>.Failure("Can't find that Product");

            CategoryHandle category= new CategoryHandle(requestProduct.CategoryName, _context);

            if (category.isNew)
                 return Result<Unit>.Failure("This Category don't exist!");

            product.Category = category.Category;

            // ensure that quantity doesnt change
            requestProduct.Quantity = product.Quantity;

            _mapper.Map(requestProduct, product);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to update Product");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
