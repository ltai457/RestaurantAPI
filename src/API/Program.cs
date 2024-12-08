using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Application.Common.Interface;

using Application.Restaurants.Commands;
using Application.Restaurants.Queries;
using Infrastructure.Persistence;

using AutoMapper;
using Application.Common.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Service registration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new OpenApiInfo 
   { 
       Title = "Restaurant API", 
       Version = "v1",
       Description = "An API for managing restaurants and dishes"
   });
});

// Database configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection"),
       x => x.MigrationsAssembly("Infrastructure"))
   .EnableServiceProviderCaching(false));

// Dependency injection
builder.Services.AddScoped<IApplicationDbContext>(provider => 
   provider.GetRequiredService<ApplicationDbContext>());



// In Program.cs
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<DatabaseSeeder>();

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Database initialization and seeding
using (var scope = app.Services.CreateScope())
{
   var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
   var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
   
   context.Database.EnsureCreated();
   await seeder.Seed();
}

// Development configurations
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API V1");
       c.RoutePrefix = string.Empty;
   });
}

// Middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// CORS configuration
app.UseCors(builder =>
{
   builder.AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader();
});

app.Run();