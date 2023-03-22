using Application.Core;
using Application.Documents.DocumentBuilder;
using Application.Documents.DocumentHelpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Entities;
using Entities.Documents;
using Entities.interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents;

public class Create
{
    public class Command : IRequest<Result<Guid>>
    {
        public DocumentDto Document { get; set; }
        public IDocumentBuilder builder {get;set;}
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Document).SetValidator(new DocumentsValidator());
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
                if (request.Document != null && request.builder != null)
                 {
                   DocumentDto requestDocument= request.Document;
                 
                    DataContextDocumentDirector director = new DataContextDocumentDirector(_context, request.builder, _userAccessor.GetUserId());
                
                    IEnumerable<DocumentLine> lines = _mapper.Map<IEnumerable<DocumentLine>>(requestDocument.DocumentLines);

                    director.SetDocument(requestDocument.CustomerId, lines, requestDocument.Date);

                    Document? document = director.BuildDocument();
                    if (document == null)
                        return Result<Guid>.Failure("Failed to create new Document. Check all values!");
                    //Ready and save
                    _context.Add(document);
                    //Response               
                    var result = await _context.SaveChangesAsync() > 0;
                    if (!result)
                        return Result<Guid>.Failure("Failed to create new Document");
                    return Result<Guid>.Success(document.Id);
                }
                else {
                    return Result<Guid>.Failure("Failed to create new Document. Document is probably Empty");
                };
            }
        }
    }
}
