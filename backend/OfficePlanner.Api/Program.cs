using Microsoft.EntityFrameworkCore;
using OfficePlanner.Api.Data;
using OfficePlanner.Api.Services;
using OfficePlanner.Api.Services.Interfaces;
using OfficePlanner.Api.Data.Models;
using OfficePlanner.Api.Data.Interfaces;
using OfficePlanner.Api.Data.Repositories;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


// Controllers
builder.Services.AddControllers();

// Sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o =>
{
    o.IdleTimeout = TimeSpan.FromMinutes(1); // how long u can be logged in, cookie / session
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

// HttpContextAccessor for service (to touch Session)
builder.Services.AddHttpContextAccessor();

// CORS (adjust origin to your frontend)
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.WithOrigins("http://turtlebase.duckdns.org")
     .AllowAnyHeader()
     .AllowAnyMethod()
     .AllowCredentials()
));

// DI: services
// builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeAccess, EmployeeAccess>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomAccess, RoomAccess>();
builder.Services.AddScoped<IEventAccess, EventAccess>();
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseSession();

app.MapControllers();

app.Run();
