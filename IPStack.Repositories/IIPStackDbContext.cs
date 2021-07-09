using IPStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IPStack.Repositories
{
    public interface IIPStackDbContext
    {
        DbSet<IPDetails> IPDetails { get; set;  }
    }
}
