﻿using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Album
{
    public class Label
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
