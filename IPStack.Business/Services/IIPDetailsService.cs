using IPStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPStack.Business.Services
{
    public interface IIPDetailsService
    {
        /// <summary>
        /// Gets the details of an IP address
        /// </summary>
        /// <param name="ip">Th</param>
        /// <returns>A <see cref="IPDetails"/></returns>
        Task<IPDetails> GetIPDetails(string ip);

        /// <summary>
        /// Gets all IP details
        /// </summary>
        /// <returns>A <see cref="IEnumerable{IPDetails}"/></returns>
        Task<IEnumerable<IPDetails>> GetAll();

        /// <summary>
        /// Creates a new job
        /// </summary>
        /// <returns></returns>
        Task<Guid> CreateJob(int batchSize);

        /// <summary>
        /// Updates a list of IP details
        /// </summary>
        /// <returns>A <see cref="bool"/>Whether updating is finished</returns>
        Task<bool> BatchUpdate(Guid jobId, IEnumerable<IPDetails> ipDetailsList);

        /// <summary>
        /// Gets the job progress
        /// </summary>
        /// <returns>A <see cref="string"/>The job progress</returns>
        Task<string> GetJobProgress(Guid jobId);
    }
}
