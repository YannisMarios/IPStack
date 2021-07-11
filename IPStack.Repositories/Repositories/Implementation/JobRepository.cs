using IPStack.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace IPStack.Repositories.Repositories.Implementation
{
    public class JobRepository : Repository<Job>, IJobRepository
    {

        public JobRepository(IPStackDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Job> GetJob(Guid id)
        {
            return await FindAsync(id);
        }
    }
}
