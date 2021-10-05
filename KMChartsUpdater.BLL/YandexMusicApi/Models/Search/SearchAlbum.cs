using KMChartsUpdater.BLL.YandexMusicApi.Models.Album;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Search
{
    public class SearchAlbum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("storageDir")]
        public string StorageDir { get; set; }

        [JsonProperty("originalReleaseYear")]
        public int OriginalReleaseYear { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("artists")]
        public ReadOnlyCollection<SearchArtist> Artists { get; set; }

        [JsonProperty("coverUri")]
        public string CoverUri { get; set; }

        [JsonProperty("trackCount")]
        public int TrackCount { get; set; }

        [JsonProperty("likesCount")]
        public long LikesCount { get; set; }
        
        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("contentWarning")]
        public string ContentWarning { get; set; }

        [JsonProperty("availableForPremiumUsers")]
        public bool AvailableForPremiumUsers { get; set; }

        [JsonProperty("trackPosition")]
        public TrackPosition TrackPosition { get; set; }

        [JsonProperty("availableRegions")]
        public ReadOnlyCollection<string> AvailableRegions { get; set; }

        [JsonProperty("regions")]
        public ReadOnlyCollection<string> Regions { get; set; }

        [JsonProperty("labels")]
        public ReadOnlyCollection<string> Labels { get; set; }
    }
}
