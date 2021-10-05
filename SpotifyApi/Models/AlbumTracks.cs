using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace SpotifyApi.Models
{
    public class AlbumTracks
    {
        [JsonProperty("items")]
        public ReadOnlyCollection<Track> Items { get; set; }
    }
}
