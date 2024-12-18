namespace ShishByzh.Application.Identity.Authentication.Queries.SignIn;

public record Query(string UserName, string Password) : IRequest<LoginResponse>;

public class LoginResponse
{
    public string? Token { get; set; }
    public UserDto? User { get; set; }
    public bool IsSuccessful { get; set; }
    public string[]? Errors { get; set; }
}