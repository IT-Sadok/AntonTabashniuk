using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Identity.Application.Authentication.Login
{
    public sealed class LoginHandler
    {
        private readonly IIdentityService identityService;
        public LoginHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task Handle(LoginCommand command)
        {
            await identityService.LoginAsync(command.Email, command.Password);
        }
    }
}
