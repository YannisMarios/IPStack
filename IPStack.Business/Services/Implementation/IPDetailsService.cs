using AutoMapper;
using IPStack.Adapter;
using IPStack.Adapter.Exceptions;
using IPStack.Domain.Entities;
using IPStack.Domain.Enums;
using IPStack.Domain.Extensions;
using IPStack.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPStack.Business.Services.Implementation
{
    public class IPDetailsService : BaseService, IIPDetailsService
    {
        #region Memebers
        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public IPDetailsService(IUnitOfWork unitOfWork, IMapper mapper, IIPInfoProvider ipInfoProvider) : base(unitOfWork, ipInfoProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Public Methods
        public async Task<IPDetails> GetIPDetails(string ip)
        {
            if(ip is null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            var ipDetails = await UnitOfWork.IPDetailsRepository.GetIPDetails(ip);

            if(ipDetails is null)
            {
                ipDetails = await IPInfoProvider.GetDetails(ip);
                UnitOfWork.IPDetailsRepository.Add(ipDetails);
                _ = await UnitOfWork.SaveChangesAsync();
            }

            return ipDetails;
        }

        public async Task<IEnumerable<IPDetails>> GetAll()
        {
            return await UnitOfWork.IPDetailsRepository.GetAllAsync();
        }

        public async Task<Guid> CreateJob(int batchSize)
        {
            var job = new Job
            {
                Id = Guid.NewGuid(),
                Progress = 0,
                Total = batchSize,
                Status = JobStatusEnum.INPROGRESS
            };

            UnitOfWork.JobRepository.Add(job);
            _ = await UnitOfWork.SaveChangesAsync();

            return job.Id;
        }

        public async Task<bool> BatchUpdate(Guid jobId, IEnumerable<IPDetails> ipDetailsList)
        {
            if(ipDetailsList is null)
            {
                throw new ArgumentNullException(nameof(ipDetailsList));
            }

            if(!ipDetailsList.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(ipDetailsList));
            }

            var job = await UnitOfWork.JobRepository.FindAsync(jobId);

            if(job is null)
            {
                throw new EntityNotFoundException($"Job with id: {jobId} does not exist");
            }

            var tasks = new List<Task>();

            foreach (var batch in ipDetailsList.Batch(1))
            {
                var ids = batch.Select(x => x.Id);
                var entities = await UnitOfWork.IPDetailsRepository.GetManyByIDs(ids);
                if (entities.Any())
                {
                    foreach (var item in batch)
                    {
                        var entity = entities.FirstOrDefault(x => x.Id == item.Id);
                        if (entity != null)
                        {
                            entity.IP = item.IP;
                            entity.City = item.City;
                            entity.Country = item.Country;
                            entity.Continent = item.Continent;
                            entity.Latitude = item.Latitude;
                            entity.Longitude = item.Longitude;
                        }
                    }
                    job.Progress += 1;
                    job.Status = job.Progress == job.Total ? JobStatusEnum.COMPLETED : JobStatusEnum.INPROGRESS;
                    UnitOfWork.IPDetailsRepository.UpdateRange(entities);
                    UnitOfWork.JobRepository.Update(job);
                    await UnitOfWork.SaveChangesAsync();
                    await Task.Delay(5000);
                }
            }

            return true;
        }

        public async Task<string> GetJobProgress(Guid jobId)
        {
            var job = await UnitOfWork.JobRepository.FindAsync(jobId);

            if (job is null)
            {
                throw new EntityNotFoundException($"Job with id: {jobId} does not exist");
            }

            if (job.Status == JobStatusEnum.COMPLETED)
            {
                throw new InvalidOperationException($"Job with id: {jobId} has completed");
            }

            return $"{job.Progress}/{job.Total}";
        }
        #endregion
    }
}
