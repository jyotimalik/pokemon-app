using Pokedex.Libs.Http.Clients;
using Pokedex.Libs.Models;
using Pokedex.Libs.Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Pokedex.Libs.Services
{
    public class ShakespeareTranslationService : BaseClient, ITranslationService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ShakespeareTranslationService> _logger;
        public ShakespeareTranslationService(IHttpClientFactory httpClientFactory, ILogger<ShakespeareTranslationService> logger)
        {
            _client = httpClientFactory.CreateClient("ShakespeareApi");
            _logger = logger;

        }

        public async Task<string> GetPokemonWithTranslation(string description)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = JsonContent.Create(new { text = description })
                };
                TranslationResponse translatedResp = await SendAsync<TranslationResponse>(_client, request);
                if (translatedResp?.Success.Total > 0)
                {
                    description = translatedResp.Contents.Translated;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Error Getting Shakespeare Translated Pokemon Information " + ex.Message;
                _logger.LogError(errorMessage);
            }

            return description;
        }
    }
}
