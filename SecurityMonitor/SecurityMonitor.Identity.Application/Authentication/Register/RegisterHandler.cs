using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Identity.Application.Authentication.Register
{
    public sealed class RegisterHandler
    {
        private readonly IIdentityService identityService;

        public RegisterHandler(IIdentityService identityService) 
        {
            this.identityService = identityService;
        }

        public async Task Handle(RegisterCommand command)
        {
            await identityService.RegisterAsync(command.Email, command.Password);
        }
    }
}
