using nnot09Mcp.Database;
using nnot09Mcp.Models;
using nnot09Mcp.Repositories;

namespace nnot09Mcp.Services
{
    public class TestService(TestRepository repository)
    {
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await repository.GetEmployeeByIdAsync(id);
        }
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await repository.GetAllEmployeesAsync();
        }
    }
}
