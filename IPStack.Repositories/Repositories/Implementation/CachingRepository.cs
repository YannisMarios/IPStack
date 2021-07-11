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
        protected CachingRepository(IConfiguration configuration, IPStackDbContext dbContext, IMemoryCache cache) : base(dbContext)
        {
            _cache = cache;
            _cacheKey = configuration.GetValue<string>("Caching:CacheKey");
            var absoluteExpiration = configuration.GetValue<int>("Caching:AbsoluteExpirationInMinutes");
            _options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(absoluteExpiration));

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a cache entry by key
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <returns><see cref="T"/>The cached entity</returns>
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

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/>The entities</returns>
        public override async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await base.GetAllAsync();
            if(entities.Any())
            {
                foreach (var entity in entities)
                {
                    AddToCache(entity);
                }
            }

            return entities;
        }

        /// <summary>
        /// Updates a list of entities
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns><see cref="IEnumerable{T}"/>The updated entities</returns>
        public override IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            var dbEntities = base.UpdateRange(entities);
            foreach(var entity in entities)
            {
                AddToCache(entity);
            }
            return dbEntities;
        }
        #endregion


        #region Protected Methods
        /// <summary>
        /// Adds an entry to the cache
        /// </summary>
        /// <param name="entry">The entry to add to the cache</param>
        protected void AddToCache(T entry)
        {
            var key = typeof(T).GetProperty(_cacheKey).GetValue(entry);
            _cache.CreateEntry(key);
            _cache.Set(key, entry, _options);
        }
        #endregion
    }
}
