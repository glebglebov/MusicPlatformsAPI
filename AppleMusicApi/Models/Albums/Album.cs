using Newtonsoft.Json;

namespace AppleMusicApi.Models.Albums
{
    public class Album
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }
}
