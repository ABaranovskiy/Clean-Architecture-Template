using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShishByzh.Application.Common.Interfaces;
using ShishByzh.Domain.Users;
using ShishByzh.Identity.Services;
using ShishByzh.Persistence;

namespace ShishByzh.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ShishByzhDbContext>()
                // Добавить провайдеры токенов для функций восстановления пароля, подтверждения email и т.д.
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddInMemoryApiScopes(SwaggerConfiguration.ApiScopes)
                .AddInMemoryIdentityResources(SwaggerConfiguration.IdentityResources)
                .AddInMemoryApiResources(SwaggerConfiguration.ApiResources)
                .AddInMemoryClients(SwaggerConfiguration.Clients);
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:issuer"],
 
                        ValidateAudience = false,
                        ValidateLifetime = true,
 
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            configuration["Jwt:secret-key"] ?? throw new InvalidOperationException("No SecretKey provided.")
                        )),
                        
                        ClockSkew = TimeSpan.Zero
                    };
        
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var token = context.Request.Headers["Authorization"].ToString();

                            Console.WriteLine($"Передан токен: {token}.");
                            
                            context.NoResult();
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            
            return services;
        }
    }
}
