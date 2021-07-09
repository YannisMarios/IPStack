using IPStack.Domain.Entities;
using System.Threading.Tasks;

namespace IPStack.Adapter
{
    public interface IIPInfoProvider
    {
        /// <summary>
        /// Gets details of an IP addrress asynchronously
        /// </summary>
        /// <param name="ip">The IP address</param>
        /// <returns>A <see cref="IPDetails"/></returns>
        public Task<IPDetails> GetDetails(string ip);
    }
}
