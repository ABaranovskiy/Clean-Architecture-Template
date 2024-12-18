using ShishByzh.Domain.Users;

namespace ShishByzh.Application.Users.Queries.GetUser
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserDto : IMapWith<User>
    {
        public string Fio { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string PasswordHash { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
    }
}
