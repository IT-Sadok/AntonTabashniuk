using Microsoft.AspNetCore.Identity;
using SecurityMonitor.Identity.Application.Authentication;
using SecurityMonitor.Identity.Application.Authentication.Login;
using SecurityMonitor.Identity.Application.Common;
using SecurityMonitor.Identity.Domain;

namespace SecurityMonitor.Identity.Infrastructure;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IJwtTokenProvider jwtTokenProvider;
    public IdentityService(
        UserManager<ApplicationUser> userManager, 
        IJwtTokenProvider jwtTokenProvider)
    {
        this.userManager = userManager;
        this.jwtTokenProvider = jwtTokenProvider;
    }

    public async Task<Result<LoginResponse>> LoginAsync(string email, string password, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<LoginResponse>.Failure("User does not exist");
        }

        var validPassword = await userManager.CheckPasswordAsync(user, password);

        if (!validPassword)
        {
            return Result<LoginResponse>.Failure("User password is incorrect");
        }

        var token = jwtTokenProvider.GenerateToken(user.Id, user.Email);
        return Result<LoginResponse>.Success(new LoginResponse(token));
    }

    public async Task<Result<bool>> RegisterAsync(string email, string password, CancellationToken ct)
    {
        var existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            return Result<bool>.Failure("User already exists");
        }

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return Result<bool>.Failure(string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        return Result<bool>.Success(true);
    }
}
