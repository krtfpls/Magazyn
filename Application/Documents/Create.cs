using Application.Core;
using Application.Documents.DocumentHelpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Entities;
using Entities.Documents;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents;

public class Create
{
     public class Command : IRequest<Result<Unit>>
    {
        public NewDocument Document { get; set; }
    }

     public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Document).SetValidator(new DocumentsValidator());
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
     //   private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)//, IUserAccessor userAccessor)
        {
            _mapper = mapper;
            _context = context;
          //  _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            Document doc = new Document();

            if ((doc.Customer= await _context.Customers.FindAsync(request.Document.newDocument.Customer.Id)) == null)
                        return Result<Unit>.Failure("Customer dont exist");
            
            doc.Date = request.Document.newDocument.Date;

            if ((doc.Type= await _context.DocumentTypes.FirstOrDefaultAsync(type => type.Name==request.Document.newDocument.Type)) == null)
                        return Result<Unit>.Failure("Wrong Document Type");
            
            doc.Number= ((await _context.Documents
                                .CountAsync(type => type.TypeId== doc.Type.Id))+1).ToString()+ "/"+ DateTime.Now.Year;
            
            var productLines = new List<DocumentLine>();

            foreach(DocumentLineDto documentLine in request.Document.newDocument.DocumentLines){
                Product product = await _context.Products
                                    .FindAsync(documentLine.Product.Id);
                                    
                if (product == null) return Result<Unit>.Failure("Failed to add product "+ documentLine.Product.Name + ". Cant Find this product");
                
                var line = request.Document.UpdateProductLine(product, documentLine.Quantity);

                productLines.Add(line);
            } 

            doc.DocumentLines=productLines;
            _context.Add(doc);
                   
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
                return Result<Unit>.Failure("Failed to create new Document");
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

}
