using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore;
using nnot09Mcp.Database;
using nnot09Mcp.Models;

namespace nnot09Mcp.Repositories
{
    public class TestRepository(TestContext ctx)
    {
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await ctx.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await ctx.Employees.ToListAsync();
        }
    }
}
