using IPStack.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPStack.Repositories.Repositories
{
    public interface IIPDetailsRepository: IRepository<IPDetails>
    {
        /// <summary>
        /// Gets the details of an IP address
        /// </summary>
        /// <param name="ip">The IP address</param>
        /// <returns>A <see cref="IPDetails"/></returns>
        Task<IPDetails> GetIPDetails(string ip);

        /// <summary>
        /// Gets a list of IP details by their ids
        /// </summary>
        /// <param name="ids">The list of ids</param>
        /// <returns>A <see cref="IEnumerable{IPDetails}"/></returns>
        Task<IEnumerable<IPDetails>> GetManyByIDs(IEnumerable<int> ids);
    }
}
