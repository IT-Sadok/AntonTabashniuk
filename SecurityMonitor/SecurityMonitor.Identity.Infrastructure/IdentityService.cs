using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SecurityMonitor.Identity.Application.Authentication;
using SecurityMonitor.Identity.Domain;

namespace SecurityMonitor.Identity.Infrastructure
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = userManager.FindByEmailAsync(email);

            if (user.Result is null)
            {
                throw new InvalidOperationException("User already exists");
            }

            var validPassword = await userManager.CheckPasswordAsync(user.Result, password);

            if (!validPassword)
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task RegisterAsync(string email, string password)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
                throw new InvalidOperationException("User already exists");
            }

            var user = new ApplicationUser
            {
                Email = email,
                UserName = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(x => x.Description)));
            }
        }
    }
}
