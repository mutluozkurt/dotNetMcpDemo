using System.Text.Json;
using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCPServer.MCPTools
{
    [McpServerToolType]
    public static class ExchangeTool
    {
        static readonly HttpClient httpClient = new();

        [McpServerTool, Description("Get the exchange rate from one currency to another")]
        public static async Task<string> GetExchangeRate(string from, string to)
        {
            var apiKey = "YOUR_API_KEY";
            var date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var url = $"https://api.exchangerate.host/convert?access_key={apiKey}&from={from}&to={to}&amount=1&date={date}";

            try
            {
                var response = await httpClient.GetStringAsync(url);
                using var json = JsonDocument.Parse(response);
                var root = json.RootElement;

                if (root.TryGetProperty("success", out var success) && success.GetBoolean())
                {
                    var rate = root.GetProperty("result").GetDecimal();
                    return $"1 {from.ToUpper()} = {rate} {to.ToUpper()} (on {date})";
                }
                else
                {
                    return "API response error.";
                }
            }
            catch (Exception ex)
            {
                return $"Exchange rate fetch error: {ex.Message}";
            }
        }
    }
}