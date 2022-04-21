using Pokedex.Libs.Http.Clients;
using Pokedex.Libs.Models;
using Pokedex.Libs.Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Pokedex.Libs.Services
{
    public class YodaTranslationService : BaseClient, ITranslationService
    {
        private readonly HttpClient _client;
        public YodaTranslationService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("YodaApi");

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
                var errorMessage = "Error Getting Yoda Translated Pokemon Information " + ex.Message;
                throw new Exception(errorMessage);
            }

            return description;
        }
    }
}
