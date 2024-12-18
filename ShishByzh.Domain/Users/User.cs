using Microsoft.AspNetCore.Identity;

namespace ShishByzh.Domain.Users
{
    public sealed class User : IdentityUser<Guid>
    {
        public string Fio { get; init; }

        public User(string fio)
        {
            Fio = fio;
        }

        public User(string fio, string? userName, string? phoneNumber)
        {
            Fio = fio;
            UserName = userName;
            PhoneNumber = phoneNumber;
        }
    }
}
