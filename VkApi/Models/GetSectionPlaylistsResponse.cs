using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace VkApi.Models
{
    public class GetSectionPlaylistsResponse
    {
        [JsonProperty("section")]
        public AudioCatalogSection Section { get; set; }

        [JsonProperty("playlists")]
        public ReadOnlyCollection<AudioCatalogAlbum> Playlists { get; set; }
    }
}
