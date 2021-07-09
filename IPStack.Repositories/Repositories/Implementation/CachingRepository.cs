using IPStack.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        protected string CacheKey { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CachingRepository{T}"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="cache">The cache.</param>
        protected CachingRepository(IConfiguration configuration, IPStackDbContext dbContext, IMemoryCache cache) : base(dbContext)
        {
            _cache = cache;
            CacheKey = typeof(T).FullName;
            var absoluteExpiration = configuration.GetValue<int>("Caching:AbsoluteExpirationInMinutes");
            var slidingExpirationInMinutes = configuration.GetValue<int>("Caching:SlidingExpirationInMinutes");
            _options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(absoluteExpiration))
                .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationInMinutes));

        }
        #endregion

        #region Public Methods
        public override T Add(T entity)
        {
            AddToCache(entity);
            return base.Add(entity);
        }

        public override IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            AddManyToCache(entities);
            return base.AddRange(entities);
        }

        public override void DeleteRange(IEnumerable<T> entities)
        {
            _cache.Remove(CacheKey);
            base.DeleteRange(entities);
        }

        public override async Task<T> FindAsync(params object[] keyValues)
        {
            var cachedEntity = _cache.Get<T>(CacheKey);

            if (cachedEntity != null)
            {
                return cachedEntity;
            } 
            else
            {
                cachedEntity = await base.FindAsync(keyValues);
                AddToCache(cachedEntity);
            }

            return cachedEntity;
        }

        public override async Task<IEnumerable<T>> GetAllAsync()
        {
            var cachedEntities = _cache.Get<IEnumerable<T>>(CacheKey);

            if (cachedEntities != null)
            {
                return cachedEntities;
            }
            else
            {
                cachedEntities = await base.GetAllAsync();
                AddManyToCache(cachedEntities);
            }
            return cachedEntities;
        }

        public override T Update(T entity)
        {
            _cache.Remove(CacheKey);
            return base.Update(entity);
        }

        public override IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _cache.Remove(CacheKey);
            return base.UpdateRange(entities);
        }
        #endregion

        #region Protected Methods
        protected void AddToCache(T entry)
        {
            _cache.Remove(CacheKey);
            _cache.Set(CacheKey, entry, _options);
        }

        protected void AddManyToCache(IEnumerable<T> entries)
        {
            _cache.Remove(CacheKey);
            _cache.Set(CacheKey, entries, _options);
        }
        #endregion
    }
}
