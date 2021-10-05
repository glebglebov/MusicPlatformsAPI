using AppleMusicApi.Models.Songs;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AppleMusicApi.Models.Chart
{
    public class SongsChart
    {
        [JsonProperty("chart")]
        public string Chart { get; set; }

        [JsonProperty("data")]
        public ReadOnlyCollection<Song> Data { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
