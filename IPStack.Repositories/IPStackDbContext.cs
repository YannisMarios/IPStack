using IPStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPStack.Repositories
{
    public class IPStackDbContext : DbContext, IIPStackDbContext
    {
        public IPStackDbContext(DbContextOptions<IPStackDbContext> options)
           : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<IPDetails> IPDetails { get; set; }
    }
}
