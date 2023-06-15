using Application.Categories;
using Application.Core;
using AutoMapper;
using Data;
using Entities;
using Entities.interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products
{
    public class Verify
    {
    public class Command : IRequest<Result<IEnumerable<ProductToVerify>>>
    {
        public  IEnumerable<ProductDto>  Products { get; set; }
    }

        public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
        
            RuleForEach(x => x.Products).SetValidator(new ProductValidator());
        }
    }
    
    public class Handler : IRequestHandler<Command, Result<IEnumerable<ProductToVerify>>>
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

        public async Task<Result<IEnumerable<ProductToVerify>>> Handle(Command request, CancellationToken cancellationToken)
        {

            var requestProducts = request.Products;

            IEnumerable<ProductToVerify> productsToVerify = await VerifyProductsAsync(requestProducts);

             return Result<IEnumerable<ProductToVerify>>.Success(productsToVerify);
        }

            private async Task<List<ProductToVerify>> VerifyProductsAsync(IEnumerable<ProductDto>  requestProducts)
            {
                var userID = _userAccessor.GetUserId();
                List<ProductToVerify> productsToVerify = new List<ProductToVerify>();

                foreach (var item in requestProducts){

                    var stockProduct = await FindProductsAsync(item, userID);

                    if (stockProduct == null)
                        {
                           productsToVerify.Add(new ProductToVerify{
                                        Name= item.Name,
                                        SerialNumber= item.SerialNumber,
                                        PriceNetto = item.PriceNetto,
                                        MinLimit = item.MinLimit != null ? item.MinLimit : 0,
                                        ImportedQuantity = item.Quantity,
                                        Quantity = 0,
                                        Description = item.Description,
                                        CategoryName = CategoryHandle.PrepareCategory(item.CategoryName, _context).Name 
                            }
                           );
                        }
                    else{
                        productsToVerify.Add(new ProductToVerify{
                                        Id= stockProduct.Id,
                                        Name= stockProduct.Name,
                                        SerialNumber= stockProduct.SerialNumber,
                                        PriceNetto = stockProduct.PriceNetto,
                                        MinLimit = stockProduct.MinLimit,
                                        Quantity = stockProduct.Quantity,
                                        ImportedQuantity = item.Quantity,
                                        Description = stockProduct.Description,
                                        CategoryName = stockProduct.Category.Name
                        });
                    }
                }
                
                return productsToVerify;
            }

            private async Task<Product?> FindProductsAsync(ProductDto product, string userId)
            {
                    var tempProduct = await _context.Products
                                .OrderBy(x => x.Name)
                                .Where(u => u.User.Id == userId)
                                .AsNoTracking()
                                .Include(c => c.Category)
                                .FirstOrDefaultAsync(x => x.Name.ToLower() == product.Name.ToLower());

                    if (tempProduct != null){
                        return tempProduct;
                    }
                    else
                    {
                        return null;
                    }
                } 
        }
}

    }
