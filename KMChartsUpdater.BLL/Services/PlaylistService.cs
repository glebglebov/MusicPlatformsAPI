using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Utils;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly UnitOfWork _uow;

        public PlaylistService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public void UpdatePlaylists(int platformId)
        {
            Platform platform = _uow.Platforms.Get(platformId);

            var api = GetApiInstance(platform);

            if (api == null)
                return;

            api.Auth();

            var playlists = (from p in _uow.Playlists.GetAll
                where p.Platform == platform
                select p).ToList();

            foreach (var playlist in playlists)
            {
                var playlistModel = api.GetPlaylist(playlist.IdOnPlatform);

                if (playlistModel == null)
                    continue;

                List<PlaylistAudioDto> playlistAudios = new List<PlaylistAudioDto>();

                foreach (var audio in playlistModel.Audios)
                {
                    string artistNormalized = Normalizer.ArtistNormalize(audio.Artist);
                    string titleNormalized = Normalizer.TitleNormalize(audio.Title);

                    PlaylistAudioDto audioDto = new PlaylistAudioDto
                    {
                        Position = audio.Position,
                        Artist = audio.Artist,
                        Title = audio.Title,
                        ArtistNormalized = artistNormalized,
                        TitleNormalized = titleNormalized
                    };

                    playlistAudios.Add(audioDto);
                }

                string cover = DeleteAndDownloadNewCover(playlist.Cover, playlistModel.CoverUrl);
                //string cover = null;

                string json = JsonConvert.SerializeObject(playlistAudios);

                playlist.Tracks = json;
                playlist.Cover = cover;

                _uow.Playlists.Update(playlist);

                Thread.Sleep(350);
            }

            _uow.Save();
        }

        public void FindTracks(int chartId)
        {
            Chart chart = _uow.Charts.Get(chartId);

            if (chart == null)
                return;

            var lastFix = chart.ChartFixes
                .OrderBy(x => x.Updated)
                .LastOrDefault();

            if (lastFix == null)
                return;

            var playlists = _uow.Playlists.GetAll
                .Where(x => x.Platform == chart.Platform)
                .ToList();

            foreach (var fix in lastFix.PositionFixes)
            {
                FindTrackInPlaylists(fix, playlists);
            }

            _uow.Save();
        }

        private string DeleteAndDownloadNewCover(string oldCoverPath, string newCoverUrl)
        {
            string oldCoverFullPath = Directory.GetCurrentDirectory() + oldCoverPath;

            try
            {
                File.Delete(oldCoverFullPath);
            }
            catch (Exception)
            {
                // ignored
            }

            string newCoverPath = ImgSaver.SaveFromUrl("/Uploads/Playlists/", newCoverUrl);

            return newCoverPath;
        }

        private void FindTrackInPlaylists(PositionFix fix, List<Playlist> playlists)
        {
            List<PlaylistDto> result = new List<PlaylistDto>();

            foreach (var playlist in playlists)
            {
                var audios = JsonConvert.DeserializeObject<List<PlaylistAudioDto>>(playlist.Tracks);

                if (audios == null)
                    continue;

                bool exist = audios.Any(x =>
                    x.TitleNormalized == fix.Audio.TitleNormalized
                    && Normalizer.ArtistEqual(fix.Audio.ArtistNormalized, x.ArtistNormalized));

                if (exist)
                {
                    result.Add(new PlaylistDto
                    {
                        Name = playlist.Name,
                        Link = playlist.Link
                    });
                }
            }

            string json = JsonConvert.SerializeObject(result);

            System.Diagnostics.Debug.WriteLine(json);

            fix.Playlists = json;
            _uow.PositionFixes.Update(fix);
        }

        private IMusicApiAdapter GetApiInstance(Platform platform)
        {
            IMusicApiAdapter api = (platform?.Id) switch
            {
                1 => ApiFactory.Instance.VkApi,
                2 => ApiFactory.Instance.SpotifyApi,
                3 => ApiFactory.Instance.YandexMusicApi,
                4 => ApiFactory.Instance.AppleMusicApi,
                _ => null,
            };

            return api;
        }
    }
}
