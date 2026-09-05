using System.ComponentModel;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using nnot09Mcp.Database;
using nnot09Mcp.Services;

namespace nnot09Mcp.Mcp
{
    [McpServerToolType]
    public static class DataTool
    {
        [McpServerTool(Name = "data-json"), Description($"Data query tool for {nameof(TestContext)}")]
        public static async Task<string> QueryDataAsync()
        {
            using (var ctx = new TestContext())
            {
                var result = await ctx.Employees.ToListAsync();
                return JsonSerializer.Serialize(result);
            }
        }

        [McpServerTool(Name = "data-csv"), Description($"Data query tool for {nameof(TestContext)}, but returning CSV.")]
        public static async Task<string> QueryWithCsvAsync()
        {
            using (var ctx = new TestContext())
            {
                var result = await ctx.Employees.ToListAsync();
                var csv = (from line in result
                           let del = ','
                           let str = $"{line.Id}{del}{line.FirstName}{del}{line.LastName}{del}{line.BirthDay}{del}{line.Age}"
                           select str);

                return string.Join(Environment.NewLine, csv);
            }
        }
    }
}
