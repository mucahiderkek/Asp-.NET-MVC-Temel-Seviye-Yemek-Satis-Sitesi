using EatWorld.Models;
using Microsoft.EntityFrameworkCore;

namespace EatWorld.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Yemek> Yemeks { get; set; }
        public DbSet<User>Users { get; set; }
    }
}
