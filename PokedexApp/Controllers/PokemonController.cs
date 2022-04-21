using Microsoft.AspNetCore.Mvc;
using Pokedex.Libs.Services;
using Pokedex.Libs.Services.Interfaces;
using System.Threading.Tasks;

namespace PokedexApi.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {

        private readonly IPokemonService _pokemonService;
        private readonly ITranslationFactory _translationFactory;
        public PokemonController(IPokemonService pokemonService, ITranslationFactory translationFactory)
        {
            _pokemonService = pokemonService;
            _translationFactory = translationFactory;
        }

        /// <summary>
        /// Get Pokemon Basic Information(Name, Description, Habitat, IsLegendary) by pokemon name
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pokemon/{pokemonName}")]
        public async Task<ObjectResult> GetPokemonBasicInfo(string pokemonName)
        {
            // check input parameter validation
            if (string.IsNullOrEmpty(pokemonName))
            {
                return new BadRequestObjectResult("Parameter pokemon name is not provided");
            }
            var pokemonResult = await _pokemonService.GetPokemonBasicData(pokemonName);
            if (pokemonResult == null)
            {
                return new NotFoundObjectResult($"No Data Found for {pokemonName}");
            }
            return new ObjectResult(pokemonResult);
        }

        /// <summary>
        /// Get Pokemon Information(Name, Description, Habitat, IsLegendary) with Fun Translation of description by pokemon name
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pokemon/translated/{pokemonName}")]
        public async Task<ObjectResult> GetTranslatedPokemonInfo(string pokemonName)
        {
            // check input parameter validation
            if (string.IsNullOrEmpty(pokemonName))
            {
                return new BadRequestObjectResult("Parameter pokemon name is not provided");
            }
            var pokemonBasicInfo = await _pokemonService.GetPokemonBasicData(pokemonName);
            if (pokemonBasicInfo == null)
            {
                return new NotFoundObjectResult($"No Data Found for {pokemonName}");
            }
            if (!string.IsNullOrEmpty(pokemonBasicInfo.Description))
            {
                ITranslationService service = _translationFactory.GetTranslationServiceObject(pokemonBasicInfo);
                pokemonBasicInfo.Description = await service.GetPokemonWithTranslation(pokemonBasicInfo.Description);
            }

            return new ObjectResult(pokemonBasicInfo);
        }
    }
}
