using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum eTicketTag
{
    Critical,
    NetworkFailure,
    HardwareFailure,
    SoftwareFailure,
    Maintenance
}
