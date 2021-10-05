using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace VkApi.Models
{
    public class GetSectionResponse
    {
        [JsonProperty("section")]
        public AudioCatalogSection Section { get; set; }

        [JsonProperty("audios")]
        public ReadOnlyCollection<AudioCatalogAudio> Audios { get; set; }
    }
}
