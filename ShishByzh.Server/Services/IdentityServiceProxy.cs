using System.Net.Http.Headers;
using System.Security.Claims;
using ShishByzh.Application.Common.Interfaces;
using ShishByzh.Application.Common.Models;
using ShishByzh.Domain.Users;

namespace ShishByzh.Server.Services;

public class IdentityServiceProxy(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : IIdentityService
{
    private const string IsInRoleCacheKeyPrefix = "IdentityService_IsInRole_";
    private const string IsInPolicyCacheKeyPrefix = "IdentityService_IsInPolicy_";
    
    public Task<string?> GetUserNameAsync(Guid userId)
    {
        var httpContext = httpContextAccessor.HttpContext;
        
        var userName = httpContext?.User.FindFirstValue(ClaimTypes.Name);

        return Task.FromResult(userName);
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        var cacheKey = $"{IsInRoleCacheKeyPrefix}{userId}_{role}";
        var httpContext = httpContextAccessor.HttpContext;

        // Проверяем кэш
        if (httpContext?.Items.TryGetValue(cacheKey, out var cachedValue) == true)
        {
            return (bool)(cachedValue ?? false);
        }
        
        // Получаем токен из заголовка запроса
        var token = httpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        // Настройка заголовков для запроса
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://identity:8081/api/authorization/is-in-role?userId={userId}&role={role}")
        {
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", token)
            }
        };

        // Выполняем запрос
        var response = await httpClient.SendAsync(requestMessage);
        
        if (response.IsSuccessStatusCode)
        {
            var isInRole = bool.Parse(await response.Content.ReadAsStringAsync());

            // Сохраняем результат в кэш
            if (httpContext != null)
            {
                httpContext.Items[cacheKey] = isInRole;
            }

            return isInRole;
        }

        return false;
    }

    public async Task<bool> IsInPolicyAsync(Guid userId, string policyName)
    {
        var cacheKey = $"{IsInPolicyCacheKeyPrefix}{userId}_{policyName}";
        var httpContext = httpContextAccessor.HttpContext;

        // Проверяем кэш
        if (httpContext?.Items.TryGetValue(cacheKey, out var cachedValue) == true)
        {
            return (bool)(cachedValue ?? false);
        }
        
        // Получаем токен из заголовка запроса
        var token = httpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        // Настройка заголовков для запроса
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://identity:8081/api/authorization/is-in-policy?userId={userId}&policy={policyName}")
        {
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", token)
            }
        };

        // Выполняем запрос
        var response = await httpClient.SendAsync(requestMessage);

        if (response.IsSuccessStatusCode)
        {
            var isInPolicy = bool.Parse(await response.Content.ReadAsStringAsync());

            // Сохраняем результат в кэш
            if (httpContext != null)
            {
                httpContext.Items[cacheKey] = isInPolicy;
            }

            return isInPolicy;
        }

        return false;
    }

    public string GenerateToken(Guid userId, string userName, string role)
    {
        throw new NotImplementedException("Token generation should be handled by the auth service.");
    }

    public Result ValidateToken(string token)
    {
        throw new NotImplementedException("Token validation should be handled by the auth service.");
    }

    public Task<(Result Result, Guid UserId)> CreateUserAsync(User user, string password, string role)
    {
        throw new NotImplementedException("User creation should be handled by the auth service.");
    }

    public Task<Result> DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException("User deletion should be handled by the auth service.");
    }

    public Task<(Result Result, User? User, string Role)> 
        SignInWithCredentials(string userName, string password, bool isPersistent = false, bool lockoutOnFailure = false)
    {
        throw new NotImplementedException("Sign-in should be handled by the auth service.");
    }

    public Task SignOut()
    {
        throw new NotImplementedException("Sign-out should be handled by the auth service.");
    }
}