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

var EnvironmentIsDevelopment= builder.Environment.IsDevelopment();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers(opt =>
{
var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
opt.Filters.Add(new AuthorizeFilter(policy));
});

//Identity
builder.Services.AddIdentityServices(builder.Configuration, EnvironmentIsDevelopment);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connString = "";
 if (EnvironmentIsDevelopment)
    connString= builder.Configuration.GetConnectionString("PostgresConnection");
 else
 {
     connString = Environment.GetEnvironmentVariable("DATABASE_URL");
 }
builder.Services.AddDbContext<DataContext>(
    opt =>
{
    opt.UseNpgsql(
        connString,
        builder => {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        }
        );
});
 
// Mediator CQRS
builder.Services.AddMediatR(typeof(List.Handler).Assembly);
//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

builder.Services.AddCors();


var app = builder.Build();

// ExceptionMiddleware- Handle logs
app.UseMiddleware<ExceptionMiddleware>();

// Payload Too Large Middleware
//app.UseMiddleware<PayloadTooLargeMiddleware>();

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
    .WithOrigins(
                "https://krtfpls.ddns.net:443",
                "http://krtfpls.ddns.net:80",
                "https://localhost:5001",
                "http://localhost:5000"));

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
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

await app.RunAsync();
