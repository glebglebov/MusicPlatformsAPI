using Newtonsoft.Json;

namespace AppleMusicApi.Models.LibraryPlaylists
{
    public class Relationships
    {
        [JsonProperty("catalog")]
        public LibraryPlaylistsCatalogRelationship Catalog { get; set; }

        [JsonProperty("tracks")]
        public LibraryPlaylistsTracksRelationship Tracks { get; set; }
    }
}
