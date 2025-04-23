using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Data;
using Scalar.AspNetCore;
using SportZoneServer.API.Middlewares;
using SportZoneServer.API.ServiceExtensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddOpenApi();
builder.Services.AddCustomServices();
builder.Services.AddControllers();

builder.Services
    .AddDbContext<ApplicationDbContext>(
        options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION")!)
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    options.AddPolicy("AllowReactFrontend", policy =>
    {
        policy
            .WithOrigins("https://sportzone-client.onrender.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

string port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.MapOpenApi();
    app.MapScalarApiReference(
        options =>
        {
            options
                .WithTheme(ScalarTheme.Moon)
                .WithDefaultHttpClient(ScalarTarget.Shell, ScalarClient.Curl);
        }
    );
}

if (app.Environment.IsProduction())
{
    app.UseCors("AllowReactFrontend");
}

app.UseHttpsRedirection();
app.MapControllers();


using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (Environment.GetEnvironmentVariable("DROP_DB_ON_RUN") == "1")
    {
        await db.Database.EnsureDeletedAsync(); 
    }
    await db.Database.MigrateAsync();
}

await DbInitializer.SeedAsync(app.Services);

app.Run();
