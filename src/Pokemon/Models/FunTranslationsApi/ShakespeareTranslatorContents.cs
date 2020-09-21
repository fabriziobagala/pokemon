using Newtonsoft.Json;

namespace Pokemon.Models.FunTranslationsApi
{
    public class ShakespeareTranslatorContents
    {
        [JsonProperty("translated")]
        public string Translated { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("translation")]
        public string Translation { get; set; }
    }
}
