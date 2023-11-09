using Microsoft.EntityFrameworkCore;

namespace MyFirstApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Department> Departments { get; set; }
        public DbSet<Service> Services { get; set; }
    }

}