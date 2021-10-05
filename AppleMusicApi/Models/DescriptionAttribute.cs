using Newtonsoft.Json;

namespace AppleMusicApi.Models
{
    public class DescriptionAttribute
    {
        [JsonProperty("short")]
        public string Short { get; set; }

        [JsonProperty("standard")]
        public string Standard { get; set; }
    }
}
