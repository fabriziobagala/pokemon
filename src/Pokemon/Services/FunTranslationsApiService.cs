using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokemon.Constants;
using Pokemon.Models.FunTranslationsApi;

namespace Pokemon.Services
{
    public class FunTranslationsApiService : IFunTranslationsApiService
    {
        private readonly ILogger<FunTranslationsApiService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FunTranslationsApiService(ILogger<FunTranslationsApiService> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<string> GetShakespeareTranslationAsync(string description)
        {
            try
            {
                // Builds the request URI.
                var endpoint = _configuration.GetValue<string>("FunTranslationsApi:Endpoint") ?? string.Empty;
                var uriBuilder = new UriBuilder(SchemeName.HTTPS, endpoint);

                // Adds the query in the request URI.
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["text"] = description;
                uriBuilder.Query = query.ToString();

                // Gets asynchronously the JSON response.
                var requestUri = uriBuilder.ToString();
                var response = await _httpClient.GetStringAsync(requestUri);

                // Returns the translated description from the deserialisation of the response.
                var shakespeareTranslator = JsonConvert.DeserializeObject<ShakespeareTranslator>(response);
                return shakespeareTranslator?.Contents?.Translated ?? string.Empty;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return string.Empty;
            }
        }
    }
}
