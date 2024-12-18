using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShishByzh.Application.Common.Interfaces;
using ShishByzh.Server.Services;

namespace ShishByzh.Server
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration configuration)
        {
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
            
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IIdentityService, IdentityServiceProxy>();
            
            return services;
        }
    }
}
