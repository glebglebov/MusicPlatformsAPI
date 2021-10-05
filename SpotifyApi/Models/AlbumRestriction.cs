using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class AlbumRestriction
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
