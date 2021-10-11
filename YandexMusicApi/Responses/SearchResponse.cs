using Newtonsoft.Json;
using YandexMusicApi.Models.Search;

namespace YandexMusicApi.Responses
{
    public class SearchResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("artists")]
        public Artists Artists { get; set; }

        [JsonProperty("albums")]
        public Albums Albums { get; set; }

        [JsonProperty("tracks")]
        public Tracks Tracks { get; set; }
    }
}
