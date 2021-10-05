using AppleMusicApi.Models.Playlists.Relationships;
using Newtonsoft.Json;

namespace AppleMusicApi.Models.Playlists
{
    public class Relationship
    {
        [JsonProperty("curator")]
        public PlaylistsCuratorRelationship Curator { get; set; }

        [JsonProperty("library")]
        public PlaylistsLibraryRelationship Library { get; set; }

        [JsonProperty("tracks")]
        public PlaylistsTracksRelationship Tracks { get; set; }
    }
}
