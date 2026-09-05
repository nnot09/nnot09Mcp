using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;
using nnot09Mcp.Database;
using nnot09Mcp.Models;

namespace nnot09Mcp.Mcp
{
    [McpServerToolType]
    public class DataTool
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public DataTool(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor http)
        {
            _client = httpClientFactory.CreateClient("mcp-data-controller");

            if (http.HttpContext is null)
            {
                throw new InvalidOperationException("HttpContext is null. This tool must be used in a web context.");
            }

            if (!http.HttpContext.Request.Host.HasValue)
            {
                throw new InvalidOperationException("HttpContext.Request.Host is null. This tool must be used in a web context.");
            }

            // holy hack
            _baseUrl = $"{http.HttpContext.Request.Scheme}://{http.HttpContext.Request.Host.Value}";
        }

        [McpServerTool(Name = "ping"), Description("Ping tool for testing connectivity.")]
        public string Ping() => $"{DateTime.Now}";

        [McpServerTool(Name = "data-json"), Description($"Data query tool for {nameof(TestContext)}")]
        public async Task<string> QueryDataAsync()
        {
            using (var ctx = new TestContext())
            {
                var result = await ctx.Employees.Select(p => p.ToDto()).ToListAsync();
                return JsonSerializer.Serialize(result);
            }
        }

        [McpServerTool(Name = "data-csv"), Description($"Data query tool for {nameof(TestContext)}, but returning CSV.")]
        public async Task<string> QueryWithCsvAsync()
        {
            using (var ctx = new TestContext())
            {
                var result = await ctx.Employees.Select(p => p.ToDto()).ToListAsync();
                var csv = (from line in result
                           let del = ','
                           let str = $"{line.FirstName}{del}{line.LastName}{del}{line.BirthDay}{del}{line.Age}"
                           select str);

                return string.Join(Environment.NewLine, csv);
            }
        }

        [McpServerTool(Name = "data-create"), Description($"Data create tool for {nameof(TestContext)}")]
        public async Task<bool> CreateEmployee(string firstName, string lastName, DateTime birthDay)
        {
            QueryBuilder qb = new QueryBuilder();
            qb.Add("firstName", firstName);
            qb.Add("lastName", lastName);
            qb.Add("birthDay", birthDay.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            
            var url = $"{_baseUrl}/api/data{qb.ToQueryString()}";
            using var response = await _client.PutAsync(url, null);
            return response.IsSuccessStatusCode;
        }
    }
}
