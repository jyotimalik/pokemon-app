using System.Threading.Tasks;

namespace Pokedex.Libs.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string> GetPokemonWithTranslation(string description);
    }
}
