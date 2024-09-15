using System.Text.Json.Serialization;

namespace RentCarApi.Domain.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransmissionType
    {
        Automatic,
        Manual,
        SemiAutomatic
    }
}