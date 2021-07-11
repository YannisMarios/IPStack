using IPStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPStack.Repositories.Repositories
{
    public interface IJobRepository: IRepository<Job>
    {
        Task<Job> GetJob(Guid id);
    }
}
