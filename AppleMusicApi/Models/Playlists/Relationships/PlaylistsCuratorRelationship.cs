using Newtonsoft.Json;

namespace AppleMusicApi.Models.Playlists.Relationships
{
    public class PlaylistsCuratorRelationship
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        // data
    }
}
