using IPStack.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPStack.Repositories.Repositories.Implementation
{
    public abstract class CachingRepository<T> : Repository<T> where T : class, IEntity
    {
        #region Members
        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>
        /// The cache.
        /// </value>
        private IMemoryCache _cache;

        /// <summary>
        /// The options
        /// </summary>
        private readonly MemoryCacheEntryOptions _options;

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <value>
        /// The cache key.
        /// </value>
        protected string _cacheKey { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CachingRepository{T}"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="cache">The cache.</param>
        protected CachingRepository(IConfiguration configuration, IPStackDbContext dbContext, IMemoryCache cache, string cacheKey) : base(dbContext)
        {
            _cache = cache;
            _cacheKey = cacheKey ?? throw new ArgumentNullException(nameof(cacheKey));
            var absoluteExpiration = configuration.GetValue<int>("Caching:AbsoluteExpirationInMinutes");
            var slidingExpirationInMinutes = configuration.GetValue<int>("Caching:SlidingExpirationInMinutes");
            _options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(absoluteExpiration))
                .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationInMinutes));

        }
        #endregion

        #region Public Methods
        public async Task<T> GetByKey(string key)
        {
            var cachedEntity = _cache.Get<T>(key);

            if(cachedEntity is null)
            {
                var predicate = GeneratePredicateForValue(_cacheKey, key);
                cachedEntity = await base.GetAsync(predicate);
                if (cachedEntity != null)
                { 
                    AddToCache(cachedEntity);
                }
            }

            return cachedEntity;
        }
        #endregion


        #region Protected Methods
        protected void AddToCache(T entry)
        {
            var key = typeof(T).GetProperty(_cacheKey).GetValue(entry);
            _cache.Remove(key);
            _cache.Set(key, entry, _options);
        }
        #endregion
    }
}
