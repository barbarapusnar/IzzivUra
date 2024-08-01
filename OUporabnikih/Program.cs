using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using OUporabnikih.Migrations;
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
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthSettings.PrivateKey))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IPasswordHasher<Uporabnik>, BCryptPasswordHasher<Uporabnik>>();
builder.Services.AddScoped<UserService>();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Uporabnik API", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Avtentikacija",
        Description = "Vnesi svoj JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http ,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Podatki API V1");
    });
}





//app.MapGet("/", () => "Hello World unauthorized!");
app.MapGet("/", () => "Hello World authorized!").RequireAuthorization();
app.MapPost("/register", async (Uporabnik userDto, UserService userService) =>
{
    var user = new Uporabnik
    {
        Ime = userDto.Ime,
        HashiranoGeslo = userService.HashPassword(userDto.HashiranoGeslo),
        JeAktiven=userDto.JeAktiven
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
        return Results.Ok(token);
    }

    return Results.Unauthorized();
});
app.MapPost("/VnosTipov", async (Tipi t, PodatkiDb db) =>
{
    var tip = new Tipi
    {
        Opis = t.Opis
      
    };

    db.Vrsta.Add(tip);
    await  db.SaveChangesAsync();
    return Results.Ok();
});
app.MapGet("/Tipi",async(PodatkiDb db)=>
   await db.Vrsta.ToListAsync()).
   RequireAuthorization();


app.Run();
