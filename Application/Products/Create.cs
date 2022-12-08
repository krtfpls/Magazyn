using Application.Core;
using AutoMapper;
using Data;
using Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class Create
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
        //private readonly IUserAccessor _userAccessor;
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
            
            Product newProduct = _mapper.Map<Product>(requestProduct);
            newProduct.User = await _context.Users.FirstOrDefaultAsync(x=> x.UserName == "admin");
//Zdecyduj czy ID nadawane na Froncie czy na backendzie
           // newProduct.Id = default;
           
            newProduct.Quantity = 0;
            newProduct.Category= await _context.Categories
                        .FirstOrDefaultAsync(x =>
                            x.Name == requestProduct.CategoryName.Trim()) ??
                    new Category() { Name = requestProduct.CategoryName.Trim() };

            _context.Products.Add(newProduct);

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
                return Result<Unit>.Failure("Failed to create new Product");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
