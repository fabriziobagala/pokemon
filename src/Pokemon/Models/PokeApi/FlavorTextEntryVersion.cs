using Newtonsoft.Json;

namespace Pokemon.Models
{
    public class FlavorTextEntryVersion
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
