﻿using Data.IRepository;
using Data.Model;
using Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>();
builder.Services.AddScoped<IRepCategory, RepCategory>();
builder.Services.AddScoped<IRepSize, RepSize>();
builder.Services.AddScoped<IRepProduct, RepProduct>();
builder.Services.AddScoped<IRepUser, RepUser>();
builder.Services.AddScoped<IRepRole, RepRole>();
builder.Services.AddScoped<IRepSupplier, RepSupplier>();

// khi include and thenincude install AddNewtonsoftJson
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// config quyền truy cập
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});
// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, // Check nhà phát hành
        ValidateAudience = true, // check đối tượng (audience) mã thông báo có khớp vs ứng dụng ko
        ValidateLifetime = true, // check token expiretime
        ValidateIssuerSigningKey = true, // check tính hợp lệ key nhà phát hành(help token trành bị giả mạo)

        ValidAudience = builder.Configuration["JWT:Audience"], // description token provider bên nào
        ValidIssuer = builder.Configuration["JWT:Issuer"], // ai là người dùng token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))   // giải mã and mã hóa token
    };
    options.Events = new JwtBearerEvents
    {
        // get mã thông báo từ cookie / allow cookie lưu trữ mã Notification
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["Token"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sử dụng HTTPS Redirection
app.UseHttpsRedirection();

// Cho phép phục vụ các tập tin tĩnh trong wwwroot
app.UseStaticFiles(); // Mặc định phục vụ từ thư mục wwwroot

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
