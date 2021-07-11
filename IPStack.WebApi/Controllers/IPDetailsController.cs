using AutoMapper;
using IPStack.Adapter.Exceptions;
using IPStack.Business.Services;
using IPStack.Domain.Entities;
using IPStack.WebApi.DTOs;
using IPStack.WorkerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
        #region Private Members

        /// <summary>
        /// The task queue
        /// </summary>
        public IBackgroundTaskQueue _queue { get; }

        /// <summary>
        /// The service scope factory
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// The IP details service
        /// </summary>
        private readonly IIPDetailsService _ipDetailsService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="IPDetailsController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public IPDetailsController(
            IMapper mapper, 
            IIPDetailsService ipDetailsService, 
            IBackgroundTaskQueue queue, 
            IServiceScopeFactory serviceScopeFactory) : base( mapper)
        {
            _ipDetailsService = ipDetailsService ?? throw new ArgumentNullException(nameof(ipDetailsService));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue)); ;
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory)); ;
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Gets the details of an IP address
        /// </summary>
        /// <param name="ip">The IP address</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpGet]
        [Route("GetIPDetails")]
        [ProducesResponseType(typeof(IPDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIPDetails([FromQuery] string ip)
        {
            try
            {
                var entity = await _ipDetailsService.GetIPDetails(ip);
                var dto = Mapper.Map<IPDetailsDTO>(entity);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all IP details
        /// </summary>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<IPDetails>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _ipDetailsService.GetAll();
            var dtos = entities.Select(x => Mapper.Map<IPDetailsDTO>(x));
            return Ok(dtos);
        }

        /// <summary>
        /// Batch updates a list of IP address details
        /// </summary>
        /// <param name="ipDetailsList">The list of IP address details</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpPost]
        [Route("BatchUpdate")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BatchUpdate([FromBody] List<IPDetailsDTO> ipDetailsList)
        {
            try
            {
                if(ipDetailsList is null)
                {
                    throw new ArgumentNullException(nameof(ipDetailsList));
                }

                if(ipDetailsList.Count == default)
                {
                    throw new ArgumentOutOfRangeException("You must provide a non-empty array of IP details");
                }

                var entities = ipDetailsList.Select(x => Mapper.Map<IPDetails>(x));
                var jobId = await  _ipDetailsService.CreateJob(ipDetailsList.Count);

                _queue.QueueBackgroundWorkItem(async token =>
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var ipdetailsService = scopedServices.GetService<IIPDetailsService>();
                    _ = await ipdetailsService.BatchUpdate(jobId, entities);
                });

                return Ok(jobId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets a job progress by id
        /// </summary>
        /// <param name="ip">The job id</param>
        /// <returns>An <see cref="IActionResult"/></returns>
        [HttpGet]
        [Route("GetJobProgress")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProgress([FromQuery] Guid jobId)
        {
            try
            {
                var progress = await _ipDetailsService.GetJobProgress(jobId);
                return Ok(progress);
            } 
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Ok(ex.Message);
            }
        }
        #endregion

    }
}
