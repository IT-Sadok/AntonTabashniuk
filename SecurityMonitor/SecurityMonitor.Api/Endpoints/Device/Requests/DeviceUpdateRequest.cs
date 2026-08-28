using SecurityMonitor.Api.Endpoints.Device.Groups.Zone.Requests;
using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Api.Endpoints.Device.Requests;

public record DeviceUpdateRequest(
    int DeviceId,
    string SerialNumber,
    DeviceType DeviceType,
    DeviceState DeviceState,
    UpdateZonesRequest Zones
    );
