using Newtonsoft.Json;

namespace VkApi.Models
{
    public class AudioCatalogAudioAds
    {
        [JsonProperty("content_id")]
        public string ContentId { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("account_age_type")]
        public int AccountAgeType { get; set; }

        [JsonProperty("puid1")]
        public int Puid1 { get; set; }

        [JsonProperty("puid22")]
        public int Puid22 { get; set; }
    }
}
