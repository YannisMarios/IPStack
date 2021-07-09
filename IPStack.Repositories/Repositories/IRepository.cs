using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPStack.Repositories.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The added entity.</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds multiple entities. Only use for 4 or more inserts.
        /// </summary>
        /// <param name="entities">The entities.</param>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes multiple entities in a single SQL statement.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Finds the entity by primary key asynchronous.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>A <see cref="TEntity"/></returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{TEntity}"/></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The updated entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>A <see cref="IEnumerable{TEntity}"/></returns>
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
    }
}
