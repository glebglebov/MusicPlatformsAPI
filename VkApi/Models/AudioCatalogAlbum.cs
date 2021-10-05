using Newtonsoft.Json;
using System.Collections.ObjectModel;
using VkApi.Models;

namespace VkApi.Models
{
    public class AudioCatalogAlbum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("owner_id")]
        public long OwnerId { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("followers")]
        public long Followers { get; set; }

        [JsonProperty("plays")]
        public long Plays { get; set; }

        [JsonProperty("create_time")]
        public long CreateTime { get; set; }

        [JsonProperty("update_time")]
        public long UpdateTime { get; set; }

        [JsonProperty("genres")]
        public ReadOnlyCollection<AudioGenre> Genres { get; set; }

        [JsonProperty("is_following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("photo")]
        public AudioThumb Photo { get; set; }

        [JsonProperty("permissions")]
        public AudioPermissions Permissions { get; set; }

        [JsonProperty("subtitle_badge")]
        public bool SubtitleBadge { get; set; }

        [JsonProperty("play_button")]
        public bool PlayButton { get; set; }

        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("album_type")]
        public string AlbumType { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("is_explicit")]
        public bool IsExplicit { get; set; }

        [JsonProperty("main_artists")]
        public ReadOnlyCollection<AudioArtist> MainArtists { get; set; }

        [JsonProperty("audio_chart_info")]
        public AudioChartInfo AudioChartInfo { get; set; }
    }
}
