using AutoMapper;
using IPStack.Adapter;
using IPStack.Domain.Entities;
using IPStack.Repositories.Repositories;
using IPStack.UoW;
using System;
using System.Threading.Tasks;

namespace IPStack.Business.Implementation
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
        #endregion
    }
}
