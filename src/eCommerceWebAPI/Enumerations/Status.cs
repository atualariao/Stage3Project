using System.Text.Json.Serialization;

namespace eCommerceWebAPI.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Pending,
        Processed
    }
}
