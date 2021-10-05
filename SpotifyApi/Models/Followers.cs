using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class Followers
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
