using Application.Core;
using AutoMapper;
using Data;
using Entities;
using Entities.interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products;

public class Create
{
    public class Command : IRequest<Result<Guid>>
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
    public class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _mapper = mapper;
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var requestProduct = request.Product;
            
            Product newProduct = _mapper.Map<Product>(requestProduct);
            newProduct.User = await _context.Users.FirstOrDefaultAsync(x=> x.Id == _userAccessor.GetUserId());

           string category = requestProduct.CategoryName.Trim().ToLower();
            newProduct.Quantity = 0;
            newProduct.Category= await _context.Categories
                        .FirstOrDefaultAsync(x =>
                            x.Name == category) ??
                    new Category() { Name = category};
            //Zdecyduj czy ID nadawane na Froncie czy na backendzie
            newProduct.Id = new Guid();
            _context.Products.Add(newProduct);

            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
                return Result<Guid>.Failure("Failed to create new Product");
            return Result<Guid>.Success(newProduct.Id);
        }
    }
}
