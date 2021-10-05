using System.Collections.ObjectModel;
using AppleMusicApi.Models.LibraryPlaylists;
using Newtonsoft.Json;

namespace AppleMusicApi.Models.Playlists.Relationships
{
    public class PlaylistsLibraryRelationship
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("data")]
        public ReadOnlyCollection<LibraryPlaylist> Data { get; set; }
    }
}
