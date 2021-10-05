using System;
using System.Collections.Generic;
using System.Linq;
using KMChartsUpdater.BLL.Charts.Models;
using KMChartsUpdater.BLL.Config;
using KMChartsUpdater.BLL.Interfaces;
using SpotifyApi;

namespace KMChartsUpdater.BLL.Adapters
{
    public class SpotifyApiAdapter : IMusicApiAdapter
    {
        private readonly Spotify _api;
        private readonly MyConfig _config;

        public SpotifyApiAdapter(MyConfig config)
        {
            _api = new Spotify();
            _config = config;
        }

        public void Auth()
        {
            _api.Auth("02e781a4f005402f8f4ca505f5b1e6da", "2048ff16164f4558a76f659b4109a654");
        }

        public List<UnifiedAudioModel> GetChart(string type)
        {
            string playlistId;

            switch (type)
            {
                case "spotify_ru":
                    playlistId = _config.Charts.Spotify["RuChartPlaylistId"];
                    break;
                case "spotify_viral_ru":
                    playlistId = _config.Charts.Spotify["RuViralPlaylistId"];
                    break;
                default:
                    return null;
            }

            var playlist = GetPlaylist(playlistId);

            return playlist.Audios;
        }

        public UnifiedPlaylistModel GetPlaylist(string playlistId)
        {
            var playlistResponse = _api.GetPlaylist(playlistId, "RU");

            List<UnifiedAudioModel> result = new List<UnifiedAudioModel>();

            int i = 0;

            foreach (var audio in playlistResponse.Tracks.Items)
            {
                ++i;

                if (audio.Track == null)
                    continue;

                result.Add(UnifiedAudioModel.Create(audio.Track, i));
            }

            var image = playlistResponse.Images.FirstOrDefault();

            var playlist = new UnifiedPlaylistModel
            {
                CoverUrl = image?.Url,
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
