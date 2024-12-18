using ShishByzh.Domain.Users;

namespace ShishByzh.Application.Users.Queries.GetUsers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserDto : IMapWith<User>
    {
        public string Fio { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
    }
}
