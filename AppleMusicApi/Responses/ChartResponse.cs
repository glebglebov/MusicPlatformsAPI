using AppleMusicApi.Models.Chart;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AppleMusicApi.Responses
{
    public class ChartResponse
    {
        [JsonProperty("results")]
        public Results Results { get; set; }
    }
}
