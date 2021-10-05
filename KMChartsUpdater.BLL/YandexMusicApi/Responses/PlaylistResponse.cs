using KMChartsUpdater.BLL.YandexMusicApi.Models.Playlist;
using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Responses
{
    public class PlaylistResponse
    {
        [JsonProperty("playlist")]
        public Playlist Playlist { get; set; }
    }
}
