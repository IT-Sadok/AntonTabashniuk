using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMonitor.Identity.Application.Authentication;

public interface IIdentityService
{
    Task RegisterAsync(string email,string password);
    Task LoginAsync(string email,string password);
}
