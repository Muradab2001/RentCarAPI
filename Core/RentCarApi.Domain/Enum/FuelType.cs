using System.Text.Json.Serialization;

namespace RentCarApi.Domain.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FuelType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid,
        LPG
    }
}