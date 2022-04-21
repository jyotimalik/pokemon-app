using Newtonsoft.Json;

namespace Pokedex.Libs.Models
{
    public class PokemonDescription
    {
        [JsonProperty("flavor_text")]
        public string Description { get; set; }
        public Language Language { get; set; }
    }
}
