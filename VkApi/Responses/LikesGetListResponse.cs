using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace VkApi.Responses
{
    public class LikesGetListResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("items")]
        public ReadOnlyCollection<string> Items { get; set; }
    }
}
