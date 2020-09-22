using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokemon.Constants;
using Pokemon.Models;

namespace Pokemon.Services
{
    public class PokeApiService : IPokeApiService
    {
        private readonly ILogger<PokeApiService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PokeApiService(ILogger<PokeApiService> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<string> GetPokemonDescriptionByNameAsync(string name)
        {
            try
            {
                // Builds the request URI.
                var endpoint = _configuration.GetValue<string>("PokeApi:Endpoint") ?? string.Empty;
                var hostName = $"{endpoint}/{name}/";
                var uriBuilder = new UriBuilder(SchemeName.HTTPS, hostName);
                var requestUri = uriBuilder.ToString();

                // Gets asynchronously the JSON response.
                var response = await _httpClient.GetStringAsync(requestUri);

                // Determines the description from the deserialisation of the response.
                var pokemonSpecies = JsonConvert.DeserializeObject<PokemonSpecies>(response);
                var entries = pokemonSpecies.Entries;
                var entry = entries.FirstOrDefault(x =>
                {
                    return string.Equals(x.Language.Name, "en", StringComparison.OrdinalIgnoreCase) &&
                           string.Equals(x.Version.Name, "alpha-sapphire", StringComparison.OrdinalIgnoreCase);
                });

                // Returns the Pokemon description.
                return entry?.FlavorText ?? string.Empty;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return string.Empty;
            }
        }
    }
}
