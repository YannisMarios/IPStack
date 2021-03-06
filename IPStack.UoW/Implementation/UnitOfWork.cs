using IPStack.Repositories;
using IPStack.Repositories.Repositories;
using IPStack.Repositories.Repositories.Implementation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IPStack.UoW.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members
        private readonly IPStackDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private IIPDetailsRepository _ipDetailsRepository;
        private IJobRepository _jobRepository;
        #endregion

        #region Constructor
        public UnitOfWork(IConfiguration configuration, IPStackDbContext dbContext, IMemoryCache cache)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _cache = cache;
        }
        #endregion

        #region Public Methods
        public IIPDetailsRepository IPDetailsRepository
        {
            get => _ipDetailsRepository ??= new IPDetailsRepository(_configuration, _dbContext, _cache);
        }

        public IJobRepository JobRepository
        {
            get => _jobRepository ??= new JobRepository(_dbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void RollBack()
        {
            _dbContext.Dispose();
        }
        #endregion


    }
}
