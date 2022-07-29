using FluentValidation;

namespace Application.Customers;

public class CustomersValidator: AbstractValidator<CustomerDto>
{
    public CustomersValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.Street).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.StreetNumber).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.City).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.TaxNumber).MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Max 500 letters!");
    }
}

public class CustomersShortValidator: AbstractValidator<CustomerShortDto>
{
    public CustomersShortValidator() {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        RuleFor(x => x.City).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
    }
}