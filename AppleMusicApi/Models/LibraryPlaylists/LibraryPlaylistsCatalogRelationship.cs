using System.Collections.ObjectModel;
using AppleMusicApi.Models.Playlists;
using Newtonsoft.Json;

namespace AppleMusicApi.Models.LibraryPlaylists
{
    public class LibraryPlaylistsCatalogRelationship
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("data")]
        public ReadOnlyCollection<Playlist> Data { get; set; }
    }
}
