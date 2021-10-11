using Newtonsoft.Json;

namespace YandexMusicApi.Models
{
    public class Normalization
    {
        [JsonProperty("gain")]
        public double Gain { get; set; }

        [JsonProperty("peak")]
        public long Peak { get; set; }
    }
}
