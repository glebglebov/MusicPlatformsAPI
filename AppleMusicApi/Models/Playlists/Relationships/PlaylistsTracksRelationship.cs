using System.Collections.ObjectModel;
using AppleMusicApi.Models.Songs;
using Newtonsoft.Json;

namespace AppleMusicApi.Models.Playlists.Relationships
{
    public class PlaylistsTracksRelationship
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("data")]
        public ReadOnlyCollection<Song> Data { get; set; }
    }
}
