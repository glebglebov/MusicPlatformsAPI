using Newtonsoft.Json;

namespace VkApi.Models
{
    public class AudioAlbum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("owner_id")]
        public long OwnerId { get; set; }

        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("thumb")]
        public AudioThumb Thumb { get; set; }
    }
}
