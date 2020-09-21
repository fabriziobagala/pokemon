using Newtonsoft.Json;

namespace Pokemon.Models.FunTranslationsApi
{
    public class ShakespeareTranslator
    {
        [JsonProperty("success")]
        public ShakespeareTranslatorSuccess Success { get; set; }

        [JsonProperty("contents")]
        public ShakespeareTranslatorContents Contents { get; set; }
    }
}
