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
    public class YodaTranslationService : BaseClient, ITranslationService
    {
        private readonly HttpClient _client;
        private readonly ILogger<YodaTranslationService> _logger;
        public YodaTranslationService(IHttpClientFactory httpClientFactory, ILogger<YodaTranslationService> logger)
        {
            _client = httpClientFactory.CreateClient("YodaApi");
            _logger = logger;

        }

        /// <summary>
        /// Get Yoda Translated Description by passing basic description
        /// Calling Yoda api and returning translated response if getting not result then returning basic description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
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
                var errorMessage = "Error Getting Yoda Translated Pokemon Information " + ex.Message;
                _logger.LogError(errorMessage);
            }

            return description;
        }
    }
}
