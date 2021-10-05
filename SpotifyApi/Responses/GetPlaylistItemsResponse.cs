using Newtonsoft.Json;
using SpotifyApi.Models;
using System.Collections.ObjectModel;

namespace SpotifyApi.Responses
{
    public class GetPlaylistItemsResponse
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public ReadOnlyCollection<PlaylistTrack> Items { get; set; }
    }
}
