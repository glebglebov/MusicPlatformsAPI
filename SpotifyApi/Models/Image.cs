using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class Image
    {
        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }
    }
}
