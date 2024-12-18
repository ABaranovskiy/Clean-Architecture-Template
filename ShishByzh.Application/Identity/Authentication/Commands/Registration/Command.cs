namespace ShishByzh.Application.Identity.Authentication.Commands.Registration;

public class Command : IRequest<Guid>
{
    public string Fio { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}