using System.Globalization;
using Newtonsoft.Json;

const int threshold = 1_000;

var pathToJsonFile = args[0];
var firstDay = DateTime.ParseExact(args[1], "yyyy-MM-dd", new CultureInfo("da-DK"), DateTimeStyles.AssumeLocal);
var officeLat = int.Parse(args[2]);
var officeLon = int.Parse(args[3]);

Console.WriteLine($"Parsed input date: {firstDay}, Kind: {firstDay.Kind}");

var json = File.ReadAllText(pathToJsonFile);

Console.WriteLine("Deserializing...");

var root = JsonConvert.DeserializeObject<Root>(json);

var locations = root!.Locations;

Console.WriteLine($"Number of deserialized locations: {locations!.Length}");

var daysCloseToOffice = locations
    .Where(l => GetDistance(l.Latitude, l.Longitude, officeLat, officeLon) <= threshold)
    .Select(l => l.TimeStamp.LocalDateTime.Date)
    .Where(d => d >= firstDay && d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday)
    .Distinct()
    .OrderBy(d => d);

Console.WriteLine($"Number of days at the office (threshold = {threshold}): {daysCloseToOffice.Count()}");

var groups = from d in daysCloseToOffice
             group d by (d.Year, d.Month) into g
             select g;

foreach (var group in groups)
{
    Console.WriteLine($"Year: {group.Key.Year}, Month: {GetMonthName(group.Key.Month)}, Count: {group.Count()}");
}

string GetMonthName(int month) => month switch 
{
    1 => "Jan",
    2 => "Feb",
    3 => "Mar",
    4 => "Apr",
    5 => "May",
    6 => "Jun",
    7 => "Jul",
    8 => "Aug",
    9 => "Sep",
    10 => "Oct",
    11 => "Nov",
    12 => "Dec",
    _ => throw new NotImplementedException()
};

double GetDistance(long lat1, long lon1, long lat2, long lon2) => Math.Sqrt(Math.Pow(lat1 - lat2, 2) + Math.Pow(lon1 - lon2, 2));