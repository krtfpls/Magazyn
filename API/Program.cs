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
    //config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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

//app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//added by myself
app.UseRouting();

//Cors place
app.UseCors(x => x.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(
            "http://localhost:4200"
        ));


app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

//added end

//app.MapControllers();

// handle fallback to index
app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllers();
              endpoints.MapFallbackToController("Index", "Fallback");
          });

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
