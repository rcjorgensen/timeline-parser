using Newtonsoft.Json;

public record Location 
{
    [JsonProperty("latitudeE7")]
    public long Latitude { get; init; }

    [JsonProperty("longitudeE7")]
    public long Longitude { get; init; }

    [JsonProperty("timestamp")]
    public DateTimeOffset TimeStamp { get; init; }

    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalData { get; init; }
}