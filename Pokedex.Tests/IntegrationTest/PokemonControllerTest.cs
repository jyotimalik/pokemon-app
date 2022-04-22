using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Pokedex.Libs.DtoMapper;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Pokedex.Tests.IntegrationTest
{
   
    public class PokemonControllerTest : IClassFixture<WebApplicationFactory<PokedexApi.Startup>>
    {
        readonly HttpClient _client;

        public PokemonControllerTest(WebApplicationFactory<PokedexApi.Startup> application)
        {
            _client = application.CreateClient();
        }
       
        [Fact]
        public async Task GetPokemonBasicInfo_NamePassed_ReturnsOkResult()
        {
            var response = await _client.GetAsync("/pokemon/mewtwo");
            response.EnsureSuccessStatusCode();
          
            Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);
        }

        [Fact]
        public async Task GetPokemonBasicInfo_NameNotPassed_ReturnsNotFoundResult()
        {
            var response = await _client.GetAsync("/pokemon/");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPokemonBasicInfo_InValidNamePassed_ReturnsNotFoundResult()
        {
            var response = await _client.GetAsync("/pokemon/test");
            
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPokemonBasicInfo_ValidNamePassed_ReturnsValidDescription()
        {
            var response = await _client.GetAsync("/pokemon/mewtwo");
            response.EnsureSuccessStatusCode();
            var dto = JsonConvert.DeserializeObject<PokemonDto>(
                await response.Content.ReadAsStringAsync()
            );
            Assert.IsNotNull(dto?.Description);
        }

        [Fact]
        public async Task GetPokemonTranslatedInfo_NamePassed_ReturnsOkResult()
        {
            var response = await _client.GetAsync("/pokemon/translated/mewtwo");
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetPokemonTranslatedInfo_NameNotPassed_ReturnsNotFoundResult()
        {
            var response = await _client.GetAsync("/pokemon/translated/");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPokemonTranslatedInfo_InValidNamePassed_ReturnsNotFoundResult()
        {
            var response = await _client.GetAsync("/pokemon/translated/test");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPokemonTranslatedInfo_ValidNamePassed_ReturnsValidDescription()
        {
            var response = await _client.GetAsync("/pokemon/translated/mewtwo");
            response.EnsureSuccessStatusCode();
            var dto = JsonConvert.DeserializeObject<PokemonDto>(
                await response.Content.ReadAsStringAsync()
            );
            Assert.IsNotNull(dto?.Description);
        }

        [Fact]
        public async Task GetPokemonYodaTranslatedInfo_ValidNamePassed_ReturnsValidDescription()
        {
            var response = await _client.GetAsync("/pokemon/translated/zubat");
            response.EnsureSuccessStatusCode();
            var dto = JsonConvert.DeserializeObject<PokemonDto>(
                await response.Content.ReadAsStringAsync()
            );
            Assert.IsNotNull(dto?.Description);
        }
        

    }
}
