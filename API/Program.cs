using System.Reflection;
using API.Extensions;
using Application.Core;
using Application.Products;
using Data;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config => {
    config.RegisterValidatorsFromAssemblyContaining<Create>();
    //config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opts =>{
    opts.UseSqlite(
        builder.Configuration["ConnectionStrings:SqliteConnection"]
    );
});
//dodane
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession();

// Mediator CQRS
builder.Services.AddMediatR(typeof(List.Handler).Assembly);
//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

// ExceptionMiddleware- Handle logs
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//added by myself
app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();

//added end

app.UseHttpsRedirection();
app.UseAuthorization();

// to remove
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
try{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex){
    var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured during migration");
}

await app.RunAsync();
