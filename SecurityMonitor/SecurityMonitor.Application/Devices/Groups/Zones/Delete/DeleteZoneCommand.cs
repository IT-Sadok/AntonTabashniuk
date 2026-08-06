namespace SecurityMonitor.Application.Devices.Groups.Zones.Delete;

public record DeleteZoneCommand(
    int DeviceId,
    IReadOnlyList<int> ZonesIds
    );
