using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScottBrady91.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PodatkiLits") ?? "Data Source=Podatki.db";
//builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddSqlite<PodatkiDb>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourissuer",
        ValidAudience = "youraudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yoursecretkey"))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IPasswordHasher<Uporabnik>, BCryptPasswordHasher<Uporabnik>>();
builder.Services.AddScoped<UserService>();



builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "PodatkiAPI";
    config.Title = "PodatkiAPI v1";
    config.Version = "v1";
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "PodatkiAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}


app.UseHttpsRedirection();
app.MapGet("/", () => "Hello World!");
app.MapPost("/register", async (Uporabnik userDto, UserService userService) =>
{
    var user = new Uporabnik
    {
        Ime = userDto.Ime,
        HashiranoGeslo = userService.HashPassword(userDto.HashiranoGeslo)
    };

    userService.CreateUser(user);
    return Results.Ok();
});

app.MapPost("/login", async (Uporabnik loginDto, UserService userService) =>
{
    var user = userService.GetUserByUsername(loginDto.Ime);
    if (user != null && userService.VerifyPassword(loginDto.HashiranoGeslo, user.HashiranoGeslo))
    {
        var token = userService.GenerateJwtToken(user);
        return Results.Ok(new { Token = token });
    }

    return Results.Unauthorized();
});


app.Run();
