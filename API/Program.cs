using API.Extensions;
using Application.Core;
using Application.Products;
using Data;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers(opt =>
{
var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
opt.Filters.Add(new AuthorizeFilter(policy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<DataContext>(opts =>
// {
//     opts.UseSqlite(
//         builder.Configuration.GetConnectionString(SqliteConnection")
//     );
// });
var connString = "";
if (builder.Environment.IsDevelopment())
    connString = builder.Configuration.GetConnectionString("PostgresConnection");
else
{
    //connString = builder.Configuration.GetConnectionString("PostgresConnection");

    // Use connection string provided at runtime by FlyIO.
    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    // Parse connection URL to connection string for Npgsql
    connUrl = connUrl.Replace("postgres://", string.Empty);
    var pgUserPass = connUrl.Split("@")[0];
    var pgHostPortDb = connUrl.Split("@")[1];
    var pgHostPort = pgHostPortDb.Split("/")[0];
    var pgDb = pgHostPortDb.Split("/")[1];
    var pgUser = pgUserPass.Split(":")[0];
    var pgPass = pgUserPass.Split(":")[1];
    var pgHost = pgHostPort.Split(":")[0];
    var pgPort = pgHostPort.Split(":")[1];

    connString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}; SslMode=disable";
}
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseNpgsql(connString);
});
 
// Mediator CQRS
builder.Services.AddMediatR(typeof(List.Handler).Assembly);
//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

builder.Services.AddCors();
//Identity
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// ExceptionMiddleware- Handle logs
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    //app.UseHsts(); // Same but manual:
    app.Use(async (context, next) => {
        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
        await next.Invoke();
    });
}

//app.UseHttpsRedirection();

//My Addons **********************************
app.UseRouting();

app.UseDefaultFiles();
app.UseStaticFiles();

//Cors place
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
// handle fallback to index
app.MapFallbackToController("Index", "Fallback");

//My Addons end *****************************

//SeedData
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

await app.RunAsync();
