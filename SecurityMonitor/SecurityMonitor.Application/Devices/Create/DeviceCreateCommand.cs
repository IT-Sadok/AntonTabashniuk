using SecurityMonitor.Application.Devices.Groups.Zones.Create;
using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Application.Devices.Create;

public sealed record DeviceCreateCommand(
    string SerialNumber,
    DeviceType DeviceType,
    DeviceState DeviceState,
    CreateZoneCommand CreateZonesCommand
    );