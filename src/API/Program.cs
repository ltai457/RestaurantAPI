using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Application.Common.Interface;

using Application.Restaurants.Commands;
using Application.Restaurants.Queries;
using Infrastructure.Persistence;

using AutoMapper;
using Application.Common.Mappings;
using Domain.Entities;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Service registration
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
   {
      Type = SecuritySchemeType.Http,
      Scheme = "bearer"
   });
   c.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
      {
         new OpenApiSecurityScheme
         {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
         },
         new string[] {}  // This line was incorrect in your code
      }
   });
});
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new OpenApiInfo 
   { 
       Title = "Restaurant API", 
       Version = "v1",
       Description = "An API for managing restaurants and dishes"
   });
});
builder.Services.AddAuthentication("Bearer");
// Database configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection"),
       x => x.MigrationsAssembly("Infrastructure"))
   .EnableServiceProviderCaching(false));

// Dependency injection
builder.Services.AddScoped<IApplicationDbContext>(provider => 
   provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddIdentityApiEndpoints<User>()
   .AddEntityFrameworkStores<ApplicationDbContext>();


// In Program.cs
//builder.Services.AddScoped<IRestaurantService, RestaurantService>();

// More verbose but very clear registration
builder.Services.AddMediatR(cfg => 
{
   cfg.RegisterServicesFromAssembly(typeof(CreateRestaurantCommand).Assembly);
});

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
app.MapGroup("Api/Identity").MapIdentityApi<User>();
// CORS configuration
app.UseCors(builder =>
{
   builder.AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader();
});

app.Run();