using ShishByzh.Application.Common.Models;
using ShishByzh.Domain.Users;

namespace ShishByzh.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);

    Task<bool> IsInRoleAsync(Guid userId, string role);
    
    Task<bool> IsInPolicyAsync(Guid userId, string policyName);
    
    string GenerateToken(Guid userId, string userName, string role);

    Result ValidateToken(string token);

    Task<(Result Result, Guid UserId)> CreateUserAsync(User user, string password, string role);

    Task<Result> DeleteUserAsync(Guid userId);

    Task<(Result Result, User? User, string Role)> SignInWithCredentials(string userName, string password, bool isPersistent = false, bool lockoutOnFailure = false);

    Task SignOut();
}
