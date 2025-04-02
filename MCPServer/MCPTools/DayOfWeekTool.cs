using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCPServer.MCPTools
{
    [McpServerToolType]
    public static class DayOfWeekTool
    {
        [McpServerTool, Description("Get the current day of the week")]
        public static string GetDayOfWeek()
        {
            var today = DateTime.Now.ToString("dddd");
            return $"Today is {today}.";
        }
    }
}