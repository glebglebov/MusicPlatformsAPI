using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class AddedBy
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
    }
}
