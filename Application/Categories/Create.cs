using Application.Core;
using AutoMapper;
using Data;
using FluentValidation;
using MediatR;

namespace Application.Categories
{
    public class Create
    {
        public class Command : IRequest<Result<int>>
        {
            public CategoryDto Category { get; set; } = new CategoryDto();
        }

        public class CommandValidator : AbstractValidator<Command>
        {

            public CommandValidator()
            {
                RuleFor(x => x.Category).SetValidator(new CategoryValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this._mapper = mapper;
                _context = context;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                CategoryHandle newCategory = new CategoryHandle(request.Category.Name, _context);

                if (!newCategory.isNew)
                    return Result<int>.Failure("The Category already exists, with id=" + newCategory.Category.Id);
                
                    _context.Categories.Add(newCategory.Category);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<int>.Failure("Cannot to create new Category");
                return Result<int>.Success(newCategory.Category.Id);
            }
        }
    }
}
