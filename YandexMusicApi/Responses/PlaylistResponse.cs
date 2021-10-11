using Newtonsoft.Json;
using YandexMusicApi.Models.Playlist;

namespace YandexMusicApi.Responses
{
    public class PlaylistResponse
    {
        [JsonProperty("playlist")]
        public Playlist Playlist { get; set; }
    }
}
