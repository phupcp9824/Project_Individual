using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IRepCategory, RepCategory>();
builder.Services.AddScoped<IRepSize, RepSize>();
builder.Services.AddScoped<IRepProduct, RepProduct>();
builder.Services.AddScoped<IRepUser, RepUser>();
builder.Services.AddScoped<IRepRole, RepRole>();
builder.Services.AddScoped<IRepSupplier, RepSupplier>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");  

app.Run();
