using WardrobeBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace WardrobeBackend.Data
{
    public class WardrobeDbContext : DbContext
    {
        public WardrobeDbContext(DbContextOptions<WardrobeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
    }
}