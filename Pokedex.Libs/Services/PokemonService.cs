using Pokedex.Libs.DtoMapper;
using Pokedex.Libs.Http.Clients;
using Pokedex.Libs.Models;
using Pokedex.Libs.Services.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Libs.Services
{
    public class PokemonService : BaseClient, IPokemonService
    {
        private readonly HttpClient _client;

        public PokemonService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("PokemonApi");
        }

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
                throw new Exception(errorMessage);
            }
            return dto;
        }
    }
}
