using Newtonsoft.Json;

namespace Pokemon.Models.FunTranslationsApi
{
    public class ShakespeareTranslatorSuccess
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
