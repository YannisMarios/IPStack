using IPStack.Repositories.Repositories;
using System.Threading.Tasks;

namespace IPStack.UoW
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the IP Details repository.
        /// </summary>
        /// <value>
        /// The IP Details repository.
        /// </value>
        IIPDetailsRepository IPDetailsRepository { get; }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Rolls back the changes.
        /// </summary>
        void RollBack();
    }
}
