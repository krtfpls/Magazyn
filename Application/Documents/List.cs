using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents;

public class List
{
    public class Query: IRequest<Result<PagedList<DocumentToReturn>>> {
            public DocumentParams Params { get; set; }   
        }
     public class Handler : IRequestHandler<Query, Result<PagedList<DocumentToReturn>>>
        {
            DataContext _context;
            private readonly IMapper _mapper;
            //private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper) //, IUserAccessor userAccessor)
            {
                _context= context;
                _mapper = mapper;
               // _userAccessor = userAccessor;
            }
     public async Task<Result<PagedList<DocumentToReturn>>> Handle(Query request, CancellationToken cancellationToken)
            {                 
                    var query = _context.Documents
                    .OrderBy(d => d.Date)
                    // .Include(u => u.User)
                    //    .Where(u => u.User.UserName == _userAccessor.GetUsername())
                    .ProjectTo<DocumentToReturn>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .AsQueryable();

                if (request.Params.Type != null){
                    query = query.Where(t => t.Type == request.Params.Type);
                }
                 
                 return Result<PagedList<DocumentToReturn>>.Success(
                     await PagedList<DocumentToReturn>.CreateAysnc(query, request.Params.PageNumber, request.Params.PageSize)
                 );
            }
        }  
}
