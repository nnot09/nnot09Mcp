using Microsoft.EntityFrameworkCore;
using nnot09Mcp.Models;

namespace nnot09Mcp.Database
{
    public class TestContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase("ERP");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(e =>
            {
                e.HasData(
                [
                    new Employee(1, "John", "Doe", new DateOnly(1990, 1, 1)),
                    new Employee(2, "Jane", "Smith", new DateOnly(1985, 5, 15)),
                    new Employee(3, "Bob", "Johnson", new DateOnly(1978, 10, 30)),
                ]);
            });
        }
    }
}
