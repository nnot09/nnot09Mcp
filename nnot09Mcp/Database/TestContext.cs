using Microsoft.EntityFrameworkCore;
using nnot09Mcp.Models;

namespace nnot09Mcp.Database
{
    public class TestContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($@"Data source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sample.db")}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasColumnName("first_name").IsRequired();
                entity.Property(e => e.LastName).HasColumnName("last_name").IsRequired();
                entity.Property(e => e.BirthDay).HasColumnName("birthday").IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
