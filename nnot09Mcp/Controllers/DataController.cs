using Microsoft.AspNetCore.Mvc;
using nnot09Mcp.Models;
using nnot09Mcp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nnot09Mcp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly TestService _testService;

        public DataController(
            ILogger<DataController> logger,
            TestService testService)
        {
            this._logger = logger;
            this._testService = testService;
        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _testService.GetAllEmployeesAsync();
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return await _testService.GetEmployeeByIdAsync(id);
        }
    }
}
