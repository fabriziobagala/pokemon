using Newtonsoft.Json;

namespace Pokemon.Models
{
    public class PokemonSpecies
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("flavor_text_entries")]
        public FlavorTextEntry[] Entries { get; set; }
    }
}
