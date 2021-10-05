using AppleMusicApi.Models.Songs;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AppleMusicApi.Models.Search
{
    public class SongsResult
    {
        [JsonProperty("data")]
        public ReadOnlyCollection<Song> Data { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
