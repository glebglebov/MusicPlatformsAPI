﻿using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Search
{
    public class Tracks
    {
        [JsonProperty("items")]
        public ReadOnlyCollection<SearchTrack> Items { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("perPage")]
        public int PerPage { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }
    }
}
