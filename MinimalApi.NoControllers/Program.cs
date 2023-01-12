using MinimalApi.Configuration;
using MinimalApi.NoControllers.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.ConfigureSwagger();
builder.Services.AddScoped<IEndPointDefinition,WeatherForecastEndPoint>();
var app = builder.Build();


app.UseHttpsRedirection();
app.SetupSwagger();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

//This can be an extension method that scans loop over the DI and map all routes;
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetService<IEndPointDefinition>()!
            .MapRoutes(app);

app.Run();