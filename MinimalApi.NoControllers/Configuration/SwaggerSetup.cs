using Microsoft.OpenApi.Models;

namespace MinimalApi.Configuration;

public static class SwaggerSetup
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            //https://github.com/domaindrivendev/Swashbuckle.AspNetCore#enable-oauth20-flows
            options.SwaggerDoc("v1", new OpenApiInfo(){Title = "Weather Api", Version = "v1"});
            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Name = "Authorization"
,               Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme.",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                    },
                    new string[] {}
                }
            });   
        });
    }
    public static void SetupSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}