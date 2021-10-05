using Newtonsoft.Json;

namespace AppleMusicApi.Models
{
    public class Preview
    {
        [JsonProperty("artwork")]
        public Artwork Artwork { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("hlsUrl")]
        public string HlsUrl { get; set; }
    }
}
