using IPStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            : base(configuration, dbContext, cache, "IP")
        {
        }
        #endregion

        #region Public Methods
        public async Task<IPDetails> GetIPDetails(string ip)
        {
            return await GetByKey(ip);
        }

        public async Task<IEnumerable<IPDetails>> GetManyByIDs(IEnumerable<int> ids)
        {
            return await DbContext.IPDetails.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        #endregion
    }
}
