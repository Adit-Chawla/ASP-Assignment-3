using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MyFirstApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Department> Departments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
