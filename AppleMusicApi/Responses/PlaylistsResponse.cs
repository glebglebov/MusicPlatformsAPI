using System.Collections.ObjectModel;
using AppleMusicApi.Models.Playlists;
using Newtonsoft.Json;

namespace AppleMusicApi.Responses
{
    public class PlaylistsResponse
    {
        [JsonProperty("data")]
        public ReadOnlyCollection<Playlist> Data { get; set; }
    }
}
