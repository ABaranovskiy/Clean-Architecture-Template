using ShishByzh.Domain.Users;

namespace ShishByzh.Application.Identity.Authentication.Queries.SignIn
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserDto : IMapWith<User>
    {
        public string Fio { get; init; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
