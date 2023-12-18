using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Ecommerce.Services.Iservices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
// Registering services
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddScoped<IUser, Userservice>();


// Registering Automapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
