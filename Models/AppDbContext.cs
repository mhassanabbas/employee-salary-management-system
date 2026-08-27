using Microsoft.EntityFrameworkCore;

namespace EmpManagementSystem.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
    }
}