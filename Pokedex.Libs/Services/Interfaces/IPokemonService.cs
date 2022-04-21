using Pokedex.Libs.DtoMapper;
using System.Threading.Tasks;

namespace Pokedex.Libs.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetPokemonBasicData(string name);
    }
}
