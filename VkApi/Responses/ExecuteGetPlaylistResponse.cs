using Newtonsoft.Json;
using System.Collections.ObjectModel;
using VkApi.Models;

namespace VkApi.Responses
{
    public class ExecuteGetPlaylistResponse
    {
        [JsonProperty("audios")]
        public ReadOnlyCollection<AudioCatalogAudio> Audios { get; set; }
    }
}
