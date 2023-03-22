using API.Extensions;
using Application.Core;
using Application.Products;
using Data;
using Entities;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<Create>();
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlite(
        builder.Configuration["ConnectionStrings:SqliteConnection"]
    );
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
  //  app.UseHsts(); Same but manual:
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
