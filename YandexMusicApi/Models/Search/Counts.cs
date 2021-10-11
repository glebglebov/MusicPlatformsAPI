using Newtonsoft.Json;

namespace YandexMusicApi.Models.Search
{
    public class Counts
    {
        [JsonProperty("tracks")]
        public int Tracks { get; set; }

        [JsonProperty("directAlbums")]
        public int DirectAlbums { get; set; }

        [JsonProperty("alsoAlbums")]
        public int AlsoAlbums { get; set; }

        [JsonProperty("alsoTracks")]
        public int AlsoTracks { get; set; }
    }
}
