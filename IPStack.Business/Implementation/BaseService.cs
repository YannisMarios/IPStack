using IPStack.Adapter;
using IPStack.UoW;

namespace IPStack.Business.Implementation
{
    public class BaseService
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets IP info provider.
        /// </summary>
        /// <value>
        /// The IP info provider.
        /// </value>
        protected IIPInfoProvider IPInfoProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="ipInfoProvider">The IP info provider.</param>
        public BaseService(IUnitOfWork unitOfWork, IIPInfoProvider ipInfoProvider)
        {
            UnitOfWork = unitOfWork;
            IPInfoProvider = ipInfoProvider;
        }
    }
}
