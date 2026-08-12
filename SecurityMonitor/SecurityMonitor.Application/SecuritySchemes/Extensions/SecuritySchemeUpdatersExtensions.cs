using SecurityMonitor.Application.Devices.Extensions;
using SecurityMonitor.Domain.Administrative;

namespace SecurityMonitor.Application.SecuritySchemes.Extensions;

public static class SecuritySchemeUpdatersExtensions
{
    public static void InitializeScheme(this SecurityScheme schemeForUpdate, SecurityScheme updateScheme)
    {
        schemeForUpdate.Name = updateScheme.Name;
        schemeForUpdate.Description = updateScheme.Description;
        DeviceUpdatersExtensions.InitializeDevice(schemeForUpdate.Device, updateScheme.Device);
    }
}
