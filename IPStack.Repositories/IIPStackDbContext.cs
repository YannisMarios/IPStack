using IPStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPStack.Repositories
{
    public interface IIPStackDbContext
    {
        /// <summary>
        /// The IPDetails table
        /// </summary>
        DbSet<IPDetails> IPDetails { get; set;  }

        /// <summary>
        /// The Jobs table
        /// </summary>
        DbSet<Job> Jobs { get; set; }
    }
}
