using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AppleMusicApi.Models.Chart
{
    public class Results
    {
        [JsonProperty("songs")]
        public ReadOnlyCollection<SongsChart> Songs { get; set; }
    }
}
