using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pokedex.Libs.Models
{
    public class PokemonInformation
    {
        public string Name { get; set; }

        [JsonProperty("flavor_text_entries")]
        public List<PokemonDescription> Descriptions { get; set; }
        public PokemonHabitat Habitat { get; set; }
        [JsonProperty("Is_Legendary")]
        public bool IsLegendary { get; set; }
    }
}
