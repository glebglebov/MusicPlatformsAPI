using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class PlaylistTrack
    {
        [JsonProperty("track")]
        public Track Track { get; set; }

        [JsonProperty("added_at")]
        public string AddedAt { get; set; }

        [JsonProperty("added_by")]
        public AddedBy AddedBy { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }
    }
}
