using Newtonsoft.Json;

public record Root 
{
    [JsonProperty("locations")]
    public Location[]? Locations { get; init; }
}
