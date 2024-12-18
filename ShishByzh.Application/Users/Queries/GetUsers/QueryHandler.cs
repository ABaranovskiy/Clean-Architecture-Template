using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Users.Queries.GetUsers
{
    public class QueryHandler(IShishByzhDbContext dbContext, IMapper mapper) : IRequestHandler<Query, Vm>
    {
        public async Task<Vm> Handle(Query request, CancellationToken cancellationToken)
        {
            return new Vm
            {
                Users = await dbContext.Users
                    .ProjectToListAsync<UserDto>(mapper.ConfigurationProvider, cancellationToken)
            };
        }
    }
}
