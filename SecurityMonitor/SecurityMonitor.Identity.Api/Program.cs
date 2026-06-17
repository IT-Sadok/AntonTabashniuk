using Microsoft.EntityFrameworkCore;
using SecurityMonitor.Identity.Application.Authentication;
using SecurityMonitor.Identity.Application.Authentication.Login;
using SecurityMonitor.Identity.Application.Authentication.Register;
using SecurityMonitor.Identity.Domain;
using SecurityMonitor.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services
    .AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IIdentityService,IdentityService>(); 
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<LoginHandler>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapPost("/auth/register", 
    async (RegisterCommand command, RegisterHandler handler) =>
    {
        await handler.Handle(command);
        return Results.Ok();
    });

app.MapPost("/auth/login",
    async (LoginCommand command, LoginHandler handler) =>
    {
        await handler.Handle(command);
        return Results.Ok();
    });

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
