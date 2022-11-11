using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Data;
using MediatR;

namespace Application.Documents;

public class Details
{
    public class Query : IRequest<Result<DocumentDetails>>
    {
        public Guid id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<DocumentDetails>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        // private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IMapper mapper)//, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            //_userAccessor = userAccessor;
        }

        public async Task<Result<DocumentDetails>> Handle(Query request, CancellationToken cancellationToken)
        {
            var item = await _context.Documents
                    .Include(l => l.DocumentLines)
            .ProjectTo<DocumentDetails>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.id);

            if (item != null)
            return Result<DocumentDetails>.Success(item);
            else
            return Result<DocumentDetails>.Failure("Can't find that Document. Try to search again");

        }
    }
}
