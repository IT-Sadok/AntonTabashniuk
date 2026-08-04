using SecurityMonitor.Api.Endpoints.Zone;
using SecurityMonitor.Domain.Devices.Enums;

namespace SecurityMonitor.Api.Endpoints.Device.Requests;

public record DeviceUpdateRequest(
    int DeviceId,
    string SerialNumber,
    DeviceType DeviceType,
    DeviceState DeviceState,
    List<UpdateZoneRequest> Zones
    );
