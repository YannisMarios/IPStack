using IPStack.Repositories;
using IPStack.Repositories.Repositories;
using IPStack.Repositories.Repositories.Implementation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace IPStack.UoW.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members
        private readonly IPStackDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private IIPDetailsRepository _ipDetailsRepository;
        #endregion

        #region Constructor
        public UnitOfWork(IConfiguration configuration, IPStackDbContext dbContext, IMemoryCache cache)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _cache = cache;
        }

        public IIPDetailsRepository IPDetailsRepository
        {
            get => _ipDetailsRepository ??= new IPDetailsRepository(_configuration, _dbContext, _cache);
        }
        #endregion
    }
}
