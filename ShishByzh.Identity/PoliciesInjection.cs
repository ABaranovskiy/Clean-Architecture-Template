namespace ShishByzh.Identity;

public static class PoliciesInjection
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("OnlyArchitects", policy =>
                policy.RequireRole("Architect"));
        });
            
        return services;
    }
}