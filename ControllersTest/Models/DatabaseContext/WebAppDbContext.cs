using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControllersTest.Models.DatabaseContext
{
    public class WebAppDbContext : IdentityDbContext
    {
        public DbSet<WebAppUser> WebAppUsers { get; set; }
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}