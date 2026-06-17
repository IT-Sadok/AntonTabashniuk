using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Identity.Application.Authentication.Register
{
    public sealed record RegisterCommand(string Email, string Password);
}
