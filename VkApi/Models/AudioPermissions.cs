using Newtonsoft.Json;

namespace VkApi.Models
{
    public class AudioPermissions
    {
        [JsonProperty("play")]
        public bool Play { get; set; }

        [JsonProperty("share")]
        public bool Share { get; set; }

        [JsonProperty("edit")]
        public bool Edit { get; set; }

        [JsonProperty("follow")]
        public bool Follow { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("boom_download")]
        public bool BoomDownload { get; set; }

        [JsonProperty("save_as_copy")]
        public bool SaveAsCopy { get; set; }
    }
}
