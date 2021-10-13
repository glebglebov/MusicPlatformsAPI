using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using AutoMapper;
using KMChartsUpdater.BLL.Adapters;
using KMChartsUpdater.BLL.Charts.Models;
using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Utils;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly UnitOfWork _uow;

        private readonly IApiAdapterFactory _apiAdapterFactory;

        public PlaylistService(UnitOfWork uow, IApiAdapterFactory apiAdapterFactory)
        {
            _uow = uow;
            _apiAdapterFactory = apiAdapterFactory;
        }

        public List<PlaylistShortDto> GetAll()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Playlist, PlaylistShortDto>());

            var mapper = new Mapper(config);

            var playlists = mapper.Map<List<PlaylistShortDto>>(_uow.Playlists.GetAll);

            return playlists;
        }

        public void UpdatePlaylists(int platformId)
        {
            Platform platform = _uow.Platforms.Get(platformId);

            if (platform.Code == "yandex_music")
            {
                UpdateYandexPlaylists(platform);
            }
            else if (platform.Code == "vk")
            {
                UpdateVkPlaylists(platform);
            }
            else
            {
                UpdatePlaylistsDefault(platform);
            }

            _uow.Save();
        }

        private void UpdatePlaylistsDefault(Platform platform)
        {
            var api = GetApiInstance(platform);

            if (api == null)
                return;

            var playlists = (from p in _uow.Playlists.GetAll
                where p.Platform == platform
                select p).ToList();

            foreach (var playlist in playlists)
            {
                var playlistModel = api.GetPlaylist(playlist.IdOnPlatform);

                if (playlistModel == null)
                    continue;

                var tracks = GetTracksFromPlaylist(playlistModel);

                string cover = DeleteAndDownloadNewCover(playlist.Cover, playlistModel.CoverUrl);
                string json = JsonConvert.SerializeObject(tracks);

                playlist.Tracks = json;
                playlist.Cover = cover;
                playlist.Updated = DateTime.Now;

                _uow.Playlists.Update(playlist);

                Thread.Sleep(200);
            }
        }

        private void UpdateVkPlaylists(Platform platform)
        {
            var playlists = (from p in _uow.Playlists.GetAll
                where p.Platform == platform
                select p).ToList();

            VkMusicApiAdapter api = _apiAdapterFactory.CreateVkApiAdapter(false);

            int accountIndex = 0;
            int i = 1;

            foreach (var playlist in playlists)
            {
                if (i % 20 == 0)
                {
                    ++accountIndex;

                    try
                    {
                        api.SetAccount(accountIndex);
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }

                var playlistModel = api.GetPlaylist(playlist.IdOnPlatform);

                if (playlistModel == null)
                    continue;

                var tracks = GetTracksFromPlaylist(playlistModel);

                string cover = DeleteAndDownloadNewCover(playlist.Cover, playlistModel.CoverUrl);
                string json = JsonConvert.SerializeObject(tracks);

                playlist.Tracks = json;
                playlist.Cover = cover;
                playlist.Updated = DateTime.Now;

                _uow.Playlists.Update(playlist);

                ++i;

                Thread.Sleep(200);
            }
        }

        private void UpdateYandexPlaylists(Platform platform)
        {
            var playlists = (from p in _uow.Playlists.GetAll
                where p.Platform == platform
                select p).ToList();

            int proxyIndex = 0;

            YandexMusicApiAdapter api = _apiAdapterFactory.CreateYandexMusicApiAdapter(true, proxyIndex);

            int i = 1;

            foreach (var playlist in playlists)
            {
                if (i % 10 == 0)
                {
                    ++proxyIndex;

                    api = _apiAdapterFactory.CreateYandexMusicApiAdapter(true, proxyIndex);
                }

                var playlistModel = api.GetPlaylist(playlist.IdOnPlatform);

                if (playlistModel == null)
                    continue;

                var tracks = GetTracksFromPlaylist(playlistModel);

                string cover = DeleteAndDownloadNewCover(playlist.Cover, playlistModel.CoverUrl);
                string json = JsonConvert.SerializeObject(tracks);

                playlist.Tracks = json;
                playlist.Cover = cover;
                playlist.Updated = DateTime.Now;

                _uow.Playlists.Update(playlist);

                ++i;

                Thread.Sleep(200);
            }
        }

        private List<PlaylistAudioDto> GetTracksFromPlaylist(UnifiedPlaylistModel playlist)
        {
            List<PlaylistAudioDto> tracks = new List<PlaylistAudioDto>();

            foreach (var track in playlist.Audios)
            {
                string artistNormalized = Normalizer.ArtistNormalize(track.Artist);
                string titleNormalized = Normalizer.TitleNormalize(track.Title);

                PlaylistAudioDto audioDto = new PlaylistAudioDto
                {
                    Position = track.Position,
                    Artist = track.Artist,
                    Title = track.Title,
                    ArtistNormalized = artistNormalized,
                    TitleNormalized = titleNormalized
                };

                tracks.Add(audioDto);
            }

            return tracks;
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
                1 => _apiAdapterFactory.CreateVkApiAdapter(false),
                2 => _apiAdapterFactory.CreateSpotifyApiAdapter(false),
                3 => _apiAdapterFactory.CreateYandexMusicApiAdapter(false),
                4 => _apiAdapterFactory.CreateAppleMusicApiAdapter(false),
                _ => null,
            };

            return api;
        }
    }
}
