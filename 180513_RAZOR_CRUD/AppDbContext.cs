using Microsoft.EntityFrameworkCore;

namespace RAZOR_CRUD
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

    }
}