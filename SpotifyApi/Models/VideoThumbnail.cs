using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class VideoThumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
