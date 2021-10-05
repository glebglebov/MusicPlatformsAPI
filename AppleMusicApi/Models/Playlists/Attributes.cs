using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AppleMusicApi.Models.Playlists
{
    public class Attributes
    {
        [JsonProperty("artwork")]
        public Artwork Artwork { get; set; }

        [JsonProperty("curatorName")]
        public string CuratorName { get; set; }

        [JsonProperty("description")]
        public DescriptionAttribute Description { get; set; }

        [JsonProperty("isChart")]
        public bool IsChart { get; set; }

        [JsonProperty("lastModifiedDate")]
        public string LastModifiedDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playlistType")]
        public string PlaylistType { get; set; }

        [JsonProperty("playParams")]
        public PlayParameters PlayParams { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("trackTypes")]
        public ReadOnlyCollection<string> TrackTypes { get; set; }
    }
}
