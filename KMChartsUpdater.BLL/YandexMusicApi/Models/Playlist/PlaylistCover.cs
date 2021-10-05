using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Playlist
{
    public class PlaylistCover
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("custom")]
        public bool Custom { get; set; }
    }
}
