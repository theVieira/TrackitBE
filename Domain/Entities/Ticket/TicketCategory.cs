using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum eTicketCategory
{
    Daily,
    Maintenance,
    Budget,
    Delivery
}
