using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace IPStack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        #region Members
        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        protected IMapper Mapper
        {
            get;

        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        protected BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
        #endregion
    }
}
