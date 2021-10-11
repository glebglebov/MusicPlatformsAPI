using Newtonsoft.Json;

namespace YandexMusicApi.Models.Album
{
    public class TrackPosition
    {
        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }
    }
}
