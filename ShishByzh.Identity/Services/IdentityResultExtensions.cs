using Microsoft.AspNetCore.Identity;
using ShishByzh.Application.Common.Models;

namespace ShishByzh.Identity.Services;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success([])
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
    
    public static Result ToApplicationResult(this SignInResult result)
    {
        return result.Succeeded
            ? Result.Success([])
            : Result.Failure(["Ошибка входа"]);
    }
}