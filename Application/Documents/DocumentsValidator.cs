using Application.Customers;
using Application.Documents.DocumentHelpers;
using Application.Products;
using FluentValidation;

namespace Application.Documents;

public class DocumentsValidator:AbstractValidator<DocumentDto>
{
    public DocumentsValidator()
    {
        RuleFor(x => x.Type).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.Number).NotEmpty().MaximumLength(20).WithMessage("Max 20 letters!");
        RuleFor(x => x.CustomerId).NotNull();
        RuleFor(x => x.Date).NotNull().GreaterThan(DateOnly.MinValue).LessThan(DateOnly.MaxValue).WithMessage("Wrong date format");
        RuleFor(x => x.DocumentLines).NotEmpty().NotNull().WithMessage("No Document Lines!");
        RuleForEach(x => x.DocumentLines).SetValidator(new DocumentLinesValidator()).NotEmpty().NotNull().WithMessage("No Document Lines!");
    }
}

public class DocumentLinesValidator:AbstractValidator<DocumentLineDto>
{
    public DocumentLinesValidator()
    {   
        RuleFor(x => x.Quantity).NotNull().GreaterThan(0).LessThan(99999).WithMessage("Quantity must be in range 1-99999!");
        RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("Products are null!");
    }
}
