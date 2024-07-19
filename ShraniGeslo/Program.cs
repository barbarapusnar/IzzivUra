var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BazaDB") ?? "Data Source=Baza.db";
//builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddSqlite<BazaDB>(connectionString);
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "UporabnikAPI";
    config.Title = "UporabnikAPI v1";
    config.Version = "v1";
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "UporabnikAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/", () => "Hello World!");
app.MapPost("/Uporabnik", async (Uporabnik u, BazaDB db) =>
{
           string password = u.Geslo;

            byte[] saltBytes = Helper.GenerateSalt();
            // Hash the password with the salt
            string hashedPassword = Helper.HashPassword(password, saltBytes);
            string base64Salt = Convert.ToBase64String(saltBytes);

            byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);
            var up=new Uporabnik{
                Ime=u.Ime,
                Geslo=hashedPassword,
                JeAktiven=u.JeAktiven
            };
            
            
            db.Uporabniki.Add(up);
            await db.SaveChangesAsync();

    return Results.Created($"/Uporabnik/{up.Id}", up);
});

app.Run();
