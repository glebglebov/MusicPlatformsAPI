using AppleMusicApi.Models.Albums;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AppleMusicApi.Models.Search
{
    public class AlbumsResult
    {
        [JsonProperty("data")]
        public ReadOnlyCollection<Album> Data { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
