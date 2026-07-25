using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Application.Devices.Update;

public sealed record DeviceUpdateCommand(
    int DeviceId,
    string SerialNumber,
    DeviceType DeviceType,
    DeviceState DeviceState);
