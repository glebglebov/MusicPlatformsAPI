using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Album
{
    public class Album
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("metaType")]
        public string MetaType { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonProperty("coverUri")]
        public string CoverUri { get; set; }

        [JsonProperty("ogImage")]
        public string OgImage { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("buy")]
        public ReadOnlyCollection<Buy> Buy { get; set; }

        [JsonProperty("trackCount")]
        public int TrackCount { get; set; }

        [JsonProperty("likesCount")]
        public long LikesCount { get; set; }

        [JsonProperty("recent")]
        public bool Recent { get; set; }

        [JsonProperty("veryImportant")]
        public bool VeryImportant { get; set; }

        [JsonProperty("artists")]
        public ReadOnlyCollection<Artist.Artist> Artists { get; set; }

        [JsonProperty("labels")]
        public ReadOnlyCollection<Label> Labels { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("availableForPremiumUsers")]
        public bool AvailableForPremiumUsers { get; set; }

        [JsonProperty("availableForMobile")]
        public bool AvailableForMobile { get; set; }

        [JsonProperty("availablePartially")]
        public bool AvailablePartially { get; set; }

        [JsonProperty("bests")]
        public ReadOnlyCollection<string> Bests { get; set; }

        [JsonProperty("trackPosition")]
        public TrackPosition TrackPosition { get; set; }
    }
}
