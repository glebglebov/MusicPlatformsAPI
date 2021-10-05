using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace VkApi.Models
{
    public class AudioCatalogAudio
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("ads")]
        public AudioCatalogAudioAds Ads { get; set; }

        [JsonProperty("is_explicit")]
        public bool IsExplicit { get; set; }

        [JsonProperty("is_licensed")]
        public bool IsLicensed { get; set; }

        [JsonProperty("track_code")]
        public string TrackCode { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("is_hq")]
        public bool IsHQ { get; set; }

        [JsonProperty("album")]
        public AudioAlbum Album { get; set; }

        [JsonProperty("main_artists")]
        public ReadOnlyCollection<AudioArtist> MainArtists { get; set; }

        [JsonProperty("featured_artists")]
        public ReadOnlyCollection<AudioArtist> FeaturedArtists { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("audio_chart_info")]
        public AudioChartInfo AudioChartInfo { get; set; }

        [JsonProperty("short_videos_allowed")]
        public bool ShortVideosAllowed { get; set; }

        [JsonProperty("stories_allowed")]
        public bool StoriesAllowed { get; set; }

        [JsonProperty("stories_cover_allowed")]
        public bool StoriesCoverAllowed { get; set; }
    }
}
