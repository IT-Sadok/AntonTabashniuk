using Microsoft.AspNetCore.Identity;
using SecurityMonitor.Identity.Application.Authentication;
using SecurityMonitor.Identity.Application.ResultPattern;
using SecurityMonitor.Identity.Domain;

namespace SecurityMonitor.Identity.Infrastructure;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Result> LoginAsync(string email, string password, CancellationToken ct)
    {
        var user = userManager.FindByEmailAsync(email);

        if (user.Result is null)
        {
            return Result.Failure("User does not exists");
        }

        var validPassword = await userManager.CheckPasswordAsync(user.Result, password);

        if (!validPassword)
        {
            return Result.Failure("User password is incorrect");
        }

        return Result.Success();
    }

    public async Task<Result> RegisterAsync(string email, string password, CancellationToken ct)
    {
        var existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            return Result.Failure("User already exists");
        }

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return Result.Failure(string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        return Result.Success();
    }
}
