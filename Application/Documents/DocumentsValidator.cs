using Application.Customers;
using Application.Products;
using FluentValidation;

namespace Application.Documents;

public class DocumentsValidator:AbstractValidator<DocumentDto>
{
    public DocumentsValidator()
    {
        // Dodaj Walidację dla shortów !!!!!!

        RuleFor(x => x.Type).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.Number).NotEmpty().MaximumLength(20).WithMessage("Max 20 letters!");
        //Sprawdz czy zostanie wykonana walidacja gdy nie bedzie obiektu CustomersShortDto !!!!
        RuleFor(x => x.Customer).NotNull().SetValidator(new CustomersShortValidator());
        RuleFor(x => x.Date).NotNull().GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue).WithMessage("Wrong date format");
        RuleForEach(x => x.DocumentLines).SetValidator(new DocumentLinesValidator()).NotNull().WithMessage("Wrong Handle Document Lines!");
    }
}

public class DocumentLinesValidator:AbstractValidator<DocumentLineDto>
{
    public DocumentLinesValidator()
    {
        RuleFor(x => x.Quantity).NotNull().GreaterThan(0).LessThan(99999).WithMessage("Wrong quantity format!");
    //Sprawdz czy zostanie wykonana walidacja gdy nie bedzie obiektu CustomersShortDto !!!!
        RuleFor(x => x.Product).SetValidator(new ProductsShortValidator()).NotNull().WithMessage("Products are null!");
    }
}
