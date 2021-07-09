using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPStack.Repositories.Repositories.Implementation
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        #region Members
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected IPStackDbContext DbContext { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        protected Repository(IPStackDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region Public Methods
        public virtual T Add(T entity)
        {
            var change = DbContext.Set<T>().Add(entity);
            return change.Entity;
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return entities;
        }

        public virtual void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }

        public virtual async Task<T> FindAsync(params object[] keyValues)
        {
            return await DbContext.Set<T>().FindAsync(keyValues);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual T Update(T entity)
        {
            DbContext.Attach(entity);
            DbContext.Set<T>().Update(entity);
            return entity;
        }

        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AttachRange(entities);
            DbContext.Set<T>().UpdateRange(entities);
            return entities;
        }
        #endregion
    }
}
