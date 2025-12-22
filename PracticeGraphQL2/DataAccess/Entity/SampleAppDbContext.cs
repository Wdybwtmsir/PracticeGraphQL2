using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace PracticeGraphQL2.DataAccess.Entity
{
    public class SampleAppDbContext:DbContext
    {
        public SampleAppDbContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Passenger> Passenger { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Train> Train { get; set; }
    }
}
