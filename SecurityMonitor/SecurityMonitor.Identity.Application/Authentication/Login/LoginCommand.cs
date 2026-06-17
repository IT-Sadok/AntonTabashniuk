using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Identity.Application.Authentication.Login
{
    public sealed record LoginCommand(string Email, string Password);
}
