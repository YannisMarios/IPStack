using IPStack.Domain.Entities;
using System.Threading.Tasks;

namespace IPStack.Business
{
    public interface IIPDetailsService
    {
        /// <summary>
        /// Gets the details of an IP address
        /// </summary>
        /// <param name="ip">Th</param>
        /// <returns>A <see cref="IPDetails"/></returns>
        Task<IPDetails> GetIPDetails(string ip);
    }
}
