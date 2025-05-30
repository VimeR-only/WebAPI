using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
    }
}
