using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Users.Queries.GetUser
{
    public class QueryHandler(IShishByzhDbContext dbContext, IMapper mapper) : IRequestHandler<Query, UserDto>
    {
        public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(request.UserId.ToString(), nameof(UserDto));
            }

            return mapper.Map<UserDto>(entity);
        }
    }
}
