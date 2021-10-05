﻿using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Artist
{
    public class ArtistCover
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
