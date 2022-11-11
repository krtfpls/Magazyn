using FluentValidation;
using FluentValidation.Results;

namespace Application.Products;

public class ProductsValidator: AbstractValidator<ProductDto>
{
    public ProductsValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.SerialNumber).MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.PriceNetto).NotNull().GreaterThan(0).LessThan(99999)
                 .ScalePrecision(2,8, true).WithMessage("Wrong price format!");
        RuleFor(x => x.MinLimit).GreaterThanOrEqualTo(0).LessThan(99999).WithMessage("Wrong Limit format!");
        RuleFor(x => x.Quantity).NotNull().GreaterThan(0).LessThan(99999).WithMessage("Wrong quantity format!")
                .LessThanOrEqualTo(1).When(x => x.SerialNumber!.Length > 0);
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Max 500 letters!");
        RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
    }
}

public class ProductsShortValidator: AbstractValidator<ProductsShortDto>
{
    public ProductsShortValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.SerialNumber).MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.PriceNetto).NotNull().GreaterThan(0).LessThan(99999)
                 .ScalePrecision(2,8, true).WithMessage("Wrong price format!");
        //RuleFor(x => x.Quantity).NotNull().GreaterThan(0).LessThan(99999).WithMessage("Wrong quantity format!")
                //.LessThanOrEqualTo(1).When(x => x.SerialNumber.Length > 0);
        RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
    }
}