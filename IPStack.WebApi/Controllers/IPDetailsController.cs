using AutoMapper;
using IPStack.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IPStack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPDetailsController : BaseController
    {
        #region Private Members

        /// <summary>
        /// The IP details service
        /// </summary>
        private readonly IIPDetailsService _ipdetailsService;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="IPDetailsController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public IPDetailsController(IMapper mapper, IIPDetailsService ipDetailsService) : base( mapper)
        {
            _ipdetailsService = ipDetailsService ?? throw new ArgumentNullException(nameof(ipDetailsService));
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Gets the details of an IP address
        /// </summary>
        /// <param name="ip">The IP address</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleIPDetails([FromQuery] string ip)
        {
            var ipDetails = await _ipdetailsService.GetIPDetails(ip);
            return Ok(ipDetails);
        }
        #endregion

    }
}
