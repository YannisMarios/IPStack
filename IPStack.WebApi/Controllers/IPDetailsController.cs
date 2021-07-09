using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPStack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPDetailsController : BaseController
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IPDetailsController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public IPDetailsController(IMapper mapper) : base( mapper)
        {
            
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Gets the details of an IP address
        /// </summary>
        /// <param name="ipAddress">The IP address</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleIPDetails([FromQuery] string ipAddress)
        {
            
            return Ok();
        }
        #endregion

    }
}
