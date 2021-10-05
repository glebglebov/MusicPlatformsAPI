using Newtonsoft.Json;

namespace AppleMusicApi.Models.Search
{
    public class Results
    {
        [JsonProperty("albums")]
        public AlbumsResult Albums { get; set; }

        [JsonProperty("songs")]
        public SongsResult Songs { get; set; }
    }
}
