using AutoMapper;
using IPStack.Adapter.Exceptions;
using IPStack.Adapter.Model;
using IPStack.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IPStack.Adapter.Implementation
{
    public class IPInfoProvider : IIPInfoProvider
    {
        #region Members
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The IPStack API URL
        /// </summary>
        private readonly string _apiUrl;

        /// <summary>
        /// The IPStack Access Key
        /// </summary>
        private readonly string _accessKey;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="IPInfoProvider"/> class.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        /// <param name="configuration">The configuration.</param>
        public IPInfoProvider(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
        {
            _httpClient = httpClient;
            _apiUrl = configuration.GetValue<string>("IPStack:BaseUrl");
            _accessKey = configuration.GetValue<string>("IPStack:AccessKey");
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<IPDetails> GetDetails(string ip)
        {
            if(ip is null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            var uri = BuildUri(ip);
            var response = await _httpClient.GetAsync(uri);

            IPDetails details;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var deserializedResponse = JsonConvert.DeserializeObject<ApiResponse>(res);
                details = _mapper.Map<IPDetails>(deserializedResponse);
            }
            else
            {
                throw new IPServiceNotAvailableException("IPStack service is not available, please try again later");
            }

            return details;
        }
        #endregion

        #region Private Methods
        private Uri BuildUri(string ip)
        {
            if(ip is null)
            {
                throw new ArgumentNullException(nameof(ip));
            }

            return new Uri($"{_apiUrl}/{ip}?access_key={_accessKey}");
        }
        #endregion
    }
}
