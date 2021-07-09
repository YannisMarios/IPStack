using IPStack.Repositories.Repositories;

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
    }
}
