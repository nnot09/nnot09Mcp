using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nnot09Mcp.Database;
using nnot09Mcp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nnot09Mcp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            this._logger = logger;
        }

        // GET: api/<DataController>
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            using var ctx = new TestContext();
            return await ctx.Employees.AsNoTracking()
                                      .Select(e => e.ToDto())
                                      .ToListAsync();
        }

        // GET api/<DataController>/5
        [HttpGet("{id}")]
        public async Task<Results<Ok<Employee>, NotFound>> Get(int id)
        {
            using var ctx = new TestContext();
            var ent = await ctx.Employees.FindAsync(id);
            if (ent is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(ent.ToDto());
        }

        [HttpPut]
        public async Task Put(string firstName, string lastName, DateTime birthDay)
        {
            using var ctx = new TestContext();
            var ent = new EmployeeEntity
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDay = DateOnly.FromDateTime(birthDay),
                CreatedAt = DateTime.Now
            };
            ctx.Employees.Add(ent);
            await ctx.SaveChangesAsync();
        }
    }
}
