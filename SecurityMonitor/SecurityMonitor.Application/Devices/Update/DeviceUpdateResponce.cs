using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Application.Devices.Update;

public sealed record DeviceUpdateResponce(
    int DeviceId,
    string SerialNumber,
    DeviceType DeviceType,
    DeviceState DeviceState);
