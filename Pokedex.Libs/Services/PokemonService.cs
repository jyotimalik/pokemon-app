using Pokedex.Libs.DtoMapper;
using Pokedex.Libs.Http.Clients;
using Pokedex.Libs.Models;
using Pokedex.Libs.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Pokedex.Libs.Services
{
    public class PokemonService : BaseClient, IPokemonService
    {
        private readonly HttpClient _client;
        private readonly ILogger<PokemonService> _logger;
        public PokemonService(IHttpClientFactory httpClientFactory,ILogger<PokemonService> logger)
        {
            _client = httpClientFactory.CreateClient("PokemonApi");
            _logger = logger;
        }

        /// <summary>
        /// Get Pokemon Basic Information Data(Name, Description, Habitat, IsLegendary) by pokemon name
        /// Calling Pokemon api and mapping return response in dto mapper
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<PokemonDto> GetPokemonBasicData(string name)
        {
            PokemonDto dto = default;
            try
            { 
                var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress+name)
            };
            var pokemonInformation = await SendAsync<PokemonInformation>(_client, request);
            if (pokemonInformation != null)
            {
                dto = new PokemonDto
                {
                    Name = pokemonInformation.Name,
                    IsLegendary = pokemonInformation.IsLegendary,
                    Habitat = pokemonInformation.Habitat?.Name,
                    Description = pokemonInformation.Descriptions?.FirstOrDefault(x => x.Language?.Name == "en")
                       ?.Description
                };
            }
            }
            catch (Exception ex)
            {
                var errorMessage = "Error Getting Basic Pokemon Information " + ex.Message;
                _logger.LogError(errorMessage);
            }
            return dto;
        }
    }
}
