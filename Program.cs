using Newtonsoft.Json;

var path = args[0];

var json = File.ReadAllText(path);

var root = JsonConvert.DeserializeObject<Root>(json);

var locations = root?.Locations;

if (locations is null) 
{
    Console.WriteLine("Deserialization returned null");
    return;
}

Console.WriteLine($"Deserialized {locations.Length} locations");