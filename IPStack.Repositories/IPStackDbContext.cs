using IPStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPStack.Repositories
{
    public class IPStackDbContext : DbContext, IIPStackDbContext
    {
        public IPStackDbContext(DbContextOptions<IPStackDbContext> options): base(options)
        {  
        }

        public DbSet<IPDetails> IPDetails { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
