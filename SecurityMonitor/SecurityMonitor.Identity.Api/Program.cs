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

app.MapPost("/auth/register", 
    async (RegisterCommand command, RegisterHandler handler, CancellationToken ct) =>
    {
        var result = await handler.Handle(command, ct);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    });

app.MapPost("/auth/login",
    async (LoginCommand command, LoginHandler handler, CancellationToken ct) =>
    {
        var result = await handler.Handle(command, ct);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok();
    });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
