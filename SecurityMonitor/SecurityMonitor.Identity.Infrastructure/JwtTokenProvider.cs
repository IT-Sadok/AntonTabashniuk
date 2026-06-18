using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecurityMonitor.Identity.Application.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecurityMonitor.Identity.Infrastructure;

public sealed class JwtTokenProvider : IJwtTokenProvider
{
    private readonly IConfiguration configuration;

    public JwtTokenProvider(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
        
    public string GenerateToken(string userId, string email)
    {
        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var secretKey = configuration["Jwt:SecretKey"]!;

        var claims =
            new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Email, email),
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Email, email)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}