using AlphaBeratung.Models;
using g13lec6.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlphaBeratung.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            

        }
        public DbSet<Clients> clients { get; set; }
        public DbSet<Users> users { get; set; }
        public  DbSet<Dept> Depts { get; set; } = null!;
        public  DbSet<Employee> Employees { get; set; } = null!;
    }
}