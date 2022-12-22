using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalApiExperiment.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Configure
builder.Services.AddControllers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureAuth(builder.Configuration);

var app = builder.Build();


//Use
app.SetupSwagger();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.Run();