using Newtonsoft.Json;

namespace AppleMusicApi.Models.Playlists
{
    public class Playlist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("relationships")]
        public Relationship Relationships { get; set; }

        // Playlists.Views
    }
}
