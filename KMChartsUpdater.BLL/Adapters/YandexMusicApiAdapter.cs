using System;
using System.Collections.Generic;
using KMChartsUpdater.BLL.Charts.Models;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.YandexMusicApi;

namespace KMChartsUpdater.BLL.Adapters
{
    public class YandexMusicApiAdapter : IMusicApiAdapter
    {
        private readonly YandexMusic _api;

        public YandexMusicApiAdapter()
        {
            _api = new YandexMusic();
        }

        public void Auth()
        {

        }

        public List<UnifiedAudioModel> GetChart(string type)
        {
            var chart = _api.GetChart();

            List<UnifiedAudioModel> result = new List<UnifiedAudioModel>();

            foreach (var audio in chart.Chart.Tracks)
            {
                result.Add(UnifiedAudioModel.Create(audio));
            }

            return result;
        }

        public UnifiedPlaylistModel GetPlaylist(string playlistId)
        {
            var response = _api.GetPlaylist(playlistId);

            List<UnifiedAudioModel> result = new List<UnifiedAudioModel>();

            if (response == null)
                return null;

            int i = 1;

            foreach (var audio in response.Playlist.Tracks)
            {
                result.Add(UnifiedAudioModel.Create(audio, i));

                ++i;
            }

            string coverUrl = "https://" + response.Playlist.OgImage.Replace("%%", "300x300");

            var playlist = new UnifiedPlaylistModel
            {
                CoverUrl = coverUrl,
                Audios = result
            };

            return playlist;
        }

        public List<UnifiedAlbumModel> GetAlbumChart(string type)
        {
            throw new NotImplementedException();
        }

        public void GetAlbum(string albumId)
        {
            throw new NotImplementedException();
        }
    }
}
