using IPStack.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace IPStack.Repositories.Repositories.Implementation
{
    public class IPDetailsRepository: CachingRepository<IPDetails>, IIPDetailsRepository
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IPDetailsRepository"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="cache">The cache.</param>
        public IPDetailsRepository(IConfiguration configuration, IPStackDbContext dbContext, IMemoryCache cache) 
            : base(configuration, dbContext, cache)
        {
        }
        #endregion
    }
}
