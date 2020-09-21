using Newtonsoft.Json;

namespace Pokemon.Models
{
    public class FlavorTextEntry
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public FlavorTextEntryLanguage Language { get; set; }

        [JsonProperty("version")]
        public FlavorTextEntryVersion Version { get; set; }
    }
}
