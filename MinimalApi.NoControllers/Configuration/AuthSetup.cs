using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApi.Configuration;

public static class AuthSetup
{
    public static void ConfigureAuth(this IServiceCollection services,IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudiences = new[] { "Dev" },
                    ValidIssuers = new[] { "Localhost" },
                    IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Security:JwtSecret"]))
                };
            });
        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme);
            options.DefaultPolicy =
                defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser()
                    .Build();
        });
    }
}