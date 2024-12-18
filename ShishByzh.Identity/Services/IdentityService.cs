using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShishByzh.Application.Common.Interfaces;
using ShishByzh.Application.Common.Models;
using ShishByzh.Domain.Users;

namespace ShishByzh.Identity.Services;

public class IdentityService(
    UserManager<User> userManager,
    IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService,
    SignInManager<User> signInManager,
    IConfiguration configuration)
    : IIdentityService
{
    private readonly string _secretKey = 
        configuration["Jwt:secret-key"] ?? throw new InvalidOperationException("No SecretKey provided.");
    private readonly string _issuer = 
        configuration["Jwt:issuer"] ?? throw new InvalidOperationException("No Issuer provided.");
    private readonly int _expiryMinutes =
        int.Parse(configuration["Jwt:expiry-minutes"] ?? throw new InvalidOperationException("No ExpiryMinutes provided."));
    
    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        return user?.UserName;
    }
    
    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        return user != null && await userManager.IsInRoleAsync(user, role);
    }
    
    public async Task<bool> IsInPolicyAsync(Guid userId, string policyName)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return false;
        }

        var principal = await userClaimsPrincipalFactory.CreateAsync(user);

        try
        {
            var result = await authorizationService.AuthorizeAsync(principal, policyName);
            
            return result.Succeeded;
        }
        catch (InvalidOperationException e)
        {
            if (!e.Message.Contains("No policy found"))
                throw new NotSupportedException();
            return false;
        }
    }
    
    public string GenerateToken(Guid userId, string userName, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Result ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                IssuerSigningKey = key,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero // можно настроить допустимую задержку
            };

            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            // Дополнительная проверка типа токена
            if (validatedToken is JwtSecurityToken jwtToken && 
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return Result.Success([jwtToken.Subject]);
            }

            throw new SecurityTokenException("Неверный токен.");
        }
        catch (Exception exception)
        {
            return Result.Failure([exception.Message]);
        }
    }

    public async Task<(Result Result, Guid UserId)> CreateUserAsync(User user, string password, string role)
    {
        var result = await userManager.CreateAsync(user, password);
        
        await userManager.AddToRoleAsync(user, role);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        return user != null ? await DeleteUserAsync(user) : Result.Success(["Пользователь удален."]);
    }

    private async Task<Result> DeleteUserAsync(User user)
    {
        var result = await userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
    
    public async Task<(Result Result, User? User, string Role)> 
        SignInWithCredentials(string userName, string password, bool isPersistent = false, bool lockoutOnFailure = false)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return (Result.Failure(["Пользователь не найден."]), null, string.Empty);
        }
        
        var result = await signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

        var roles = await userManager.GetRolesAsync(user);
        
        return (result.ToApplicationResult(), user,roles.First());
    }

    public async Task SignOut()
    {
        await signInManager.SignOutAsync();
    }
}