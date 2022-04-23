using System;
using Pokedex.Libs.DtoMapper;
using Pokedex.Libs.Services.Interfaces;

namespace Pokedex.Libs.Services
{
    public interface ITranslationFactory
    {
        ITranslationService GetTranslationServiceObject(PokemonDto pokemonInformation);
    }

    public class TranslationFactory: ITranslationFactory
    {
        private const string CaveHabitat = "cave";
        private readonly ShakespeareTranslationService _shakespeareTranslation;
        private readonly YodaTranslationService _yodaTranslation;
        public TranslationFactory(ShakespeareTranslationService shakespeareTranslation, YodaTranslationService yodaTranslation)
        {
            _shakespeareTranslation = shakespeareTranslation;
            _yodaTranslation = yodaTranslation;
        }

        /// <summary>
        /// Getting Yoda or Shakespeare service object depending on the condition of Habitat and IsLegendary
        /// </summary>
        /// <param name="pokemonInformation"></param>
        /// <returns></returns>
        public ITranslationService GetTranslationServiceObject(PokemonDto pokemonInformation)
        {
            bool isYodaTranslationRequired = string.Equals(pokemonInformation.Habitat, CaveHabitat, StringComparison.OrdinalIgnoreCase) || pokemonInformation.IsLegendary;
            if (isYodaTranslationRequired)
            {
                return _yodaTranslation;
            }
            return _shakespeareTranslation;
        }
    }
}
