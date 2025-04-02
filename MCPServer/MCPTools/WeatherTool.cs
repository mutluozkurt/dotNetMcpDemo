using System.Text.Json;
using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCPServer.MCPTools
{
    [McpServerToolType]
    public static class WeatherTool
    {
        static readonly HttpClient httpClient = new();

        [McpServerTool, Description("Get the current weather in a city")]
        public static async Task<string> GetWeatherAsync(string city)
        {
            var apiKey = "YOUR_API_KEY";
            var url = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={Uri.EscapeDataString(city)}";

            try
            {
                var json = await httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var tempC = root.GetProperty("current").GetProperty("temp_c").GetDouble();
                var condition = root.GetProperty("current").GetProperty("condition").GetProperty("text").GetString();

                return $"It's {tempC}°C and {condition?.ToLower()} in {city}.";
            }
            catch (Exception ex)
            {
                return $"Sorry, I couldn't fetch the weather for {city}. ({ex.Message})";
            }
        }
    }
}