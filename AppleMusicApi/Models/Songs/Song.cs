using Newtonsoft.Json;

namespace AppleMusicApi.Models.Songs
{
    public class Song
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        // Songs.Relationships
    }
}
