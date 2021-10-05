using Newtonsoft.Json;

namespace AppleMusicApi.Models.LibraryPlaylists
{
    public class LibraryPlaylistsTracksRelationship
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        // data
    }
}
