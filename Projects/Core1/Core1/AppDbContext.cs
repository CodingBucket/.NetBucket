using Microsoft.EntityFrameworkCore;

namespace Core1
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

    }
}