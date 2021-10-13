using System;
using System.Collections.Generic;
using KMChartsUpdater.BLL.Charts.Models;
using KMChartsUpdater.BLL.Config;
using KMChartsUpdater.BLL.Interfaces;
using VkApi;

namespace KMChartsUpdater.BLL.Adapters
{
    public class VkMusicApiAdapter : IMusicApiAdapter
    {
        private readonly VkAudioApi _api;
        private readonly MyConfig _config;

        public VkMusicApiAdapter(VkAudioApi api, MyConfig config)
        {
            _api = api;
            _config = config;
        }

        public List<UnifiedAudioModel> GetChart(string type)
        {
            string playlistId; 

            switch (type)
            {
                case "vk_sng":
                    playlistId = _config.Charts.Vk["SngChartPlaylistId"];
                    break;
                case "vk_kz":
                    playlistId = _config.Charts.Vk["KzChartPlaylistId"];
                    break;
                default:
                    return null;
            }

            var playlist = GetPlaylist(playlistId);

            return playlist.Audios;
        }

        public UnifiedPlaylistModel GetPlaylist(string playlistId)
        {
            var playlistResponse = _api.GetPlaylistById(playlistId);
            var playlistAudioResponse = _api.ExecuteGetPlaylist(playlistId);

            List<UnifiedAudioModel> result = new List<UnifiedAudioModel>();

            int i = 1;

            foreach (var audio in playlistAudioResponse.Audios)
            {
                //System.Diagnostics.Debug.WriteLine(audio.Artist + " - " + audio.Title);
                result.Add(UnifiedAudioModel.Create(audio, i));

                ++i;
            }

            var playlist = new UnifiedPlaylistModel
            {
                CoverUrl = playlistResponse.Photo.Photo300,
                Audios = result
            };

            return playlist;
        }

        public List<UnifiedAlbumModel> GetAlbumChart(string type)
        {
            var chart = _api.GetAlbumsChartSection(_config.Charts.Vk["AlbumsChartSectionId"]);

            List<UnifiedAlbumModel> result = new List<UnifiedAlbumModel>();

            int i = 1;
            foreach (var album in chart.Playlists)
            {
                result.Add(UnifiedAlbumModel.Create(album, i));

                ++i;
            }

            return result;
        }

        public void GetAlbum(string albumId)
        {
            throw new NotImplementedException();
        }

        public void SetAccount(int accountIndex)
        {
            try
            {
                var vkCredentials = _config.Credentials.Vk[accountIndex];

                _api.GetToken(vkCredentials.Login, vkCredentials.Password);
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("can't set an account");
            }
        }
    }
}
