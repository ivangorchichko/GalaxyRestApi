using Microsoft.EntityFrameworkCore;

namespace GalaxyRestApi.DAL.Models.Context
{
    public class EnterpriseContext : DbContext
    {
        public EnterpriseContext(DbContextOptions<EnterpriseContext> options)
        : base(options)
        {
            Database.EnsureCreated();   
        }

        public DbSet<Car> Cars { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; }

        public DbSet<RegisteredEmployeeAuto> RegisteredEmployees { get; set; }
    }
}
