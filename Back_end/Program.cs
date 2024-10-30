using Data.IRepository;
using Data.Model;
using Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>();
builder.Services.AddScoped<IRepCategory, RepCategory>();
builder.Services.AddScoped<IRepSize, RepSize>(); 
builder.Services.AddScoped<IRepProduct, RepProduct>();
builder.Services.AddScoped<IRepUser, RepUser>();
builder.Services.AddScoped<IRepRole, RepRole>();


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
