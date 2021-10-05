using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace SpotifyApi.Models
{
    public class Paging<T>
    {
        [JsonProperty("items")]
        public ReadOnlyCollection<T> Items { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
