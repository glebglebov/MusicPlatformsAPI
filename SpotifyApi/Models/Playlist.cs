using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace SpotifyApi.Models
{
    public class Playlist
    {
        [JsonProperty("collaborative")]
        public string Collaborative { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public ReadOnlyCollection<Image> Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("owner")]
        //public PublicUser Owner { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; }

        [JsonProperty("tracks")]
        public Paging<PlaylistTrack> Tracks { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
