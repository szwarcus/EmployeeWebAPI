using EmployeeWebApi.Infrastracture.Configuration;
using EmployeeWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApi.Infrastracture
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeesConfiguration());
        }
    }
}