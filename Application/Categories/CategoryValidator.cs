using FluentValidation;

namespace Application.Categories
{
    public class CategoryValidator: AbstractValidator<CategoryDto>
    {
        public CategoryValidator() {
             RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Max 100 letters!");
        }
    }
}