using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}
