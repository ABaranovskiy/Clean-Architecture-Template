namespace ShishByzh.Application.Users.Queries.GetUsers;

public class Vm
{
    public IReadOnlyCollection<UserDto> Users { get; set; } = Array.Empty<UserDto>();
}