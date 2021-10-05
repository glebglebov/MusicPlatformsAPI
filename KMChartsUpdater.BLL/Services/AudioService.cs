using AppleMusicApi;
using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KMChartsUpdater.BLL.Utils;
using Newtonsoft.Json;
using KMChartsUpdater.BLL.Config;

namespace KMChartsUpdater.BLL.Services
{
    public class AudioService : IAudioService
    {
        private readonly UnitOfWork _uow;

        public AudioService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<GetAudioStatsResponse> GetAudioPositions(int id, int chartId)
        {
            var result = new List<PositionDto>();

            Chart chart = _uow.Charts.Get(chartId);
            Audio audio = _uow.Audios.Get(id);

            if (audio is null || chart is null)
                return null;

            var query = from p in _uow.PositionFixes.GetAll
                        where p.Audio == audio && p.Chart == chart
                        select new
                        {
                            pos = p.Position,
                            upd = p.Date
                        };

            var audioFixes = await query.ToListAsync();

            int days = audioFixes.Count();
            int best = audioFixes.Select(x => x.pos).Min();

            DateTime lastFix = audioFixes.Select(x => x.upd).Max();

            DateTime firstFix = audioFixes.Select(x => x.upd).Min();
            var first = audioFixes
                    .FirstOrDefault(x => x.upd == firstFix);

            int m = days / 50;
            m = (m < 1) ? 1 : m;

            DateTime last = new DateTime(lastFix.Year, lastFix.Month, lastFix.Day);

            //DateTime monthAgo = last.AddDays(-m * 50);
            //monthAgo = (firstFix > monthAgo) ? firstFix : monthAgo;

            DateTime day = new DateTime(firstFix.Year, firstFix.Month, firstFix.Day);

            while (day <= last)
            {
                string date = day.ToString("dd.MM.yyyy");

                var fix = audioFixes
                    .FirstOrDefault(x => x.upd.ToString("dd.MM.yyyy") == date);

                int position = fix is null ? 200 : fix.pos;

                result.Add(new PositionDto
                {
                    Date = date,
                    Position = position
                });

                day = day.AddDays(m);
            }

            var playlists = GetPlaylistsWithThisTrack(chart.Platform, audio);

            var stats = new ItemStatsDto
            {
                Chart = chart.Name,
                Best = best,
                Days = days,
                First = firstFix.ToString("dd.MM.yyyy"),
                FirstPlace = first.pos,
                Playlists = playlists
            };

            var response = new GetAudioStatsResponse
            {
                Stats = stats,
                Count = chart.Count,
                Items = result
            };

            return response;
        }

        public void ChangeAudio(int one, int two, int chartId)
        {
            Audio a = _uow.Audios.Get(one);
            Audio b = _uow.Audios.Get(two);

            if (a == null || b == null)
                return;

            var fixes = new List<PositionFix>();

            if (chartId == 0)
            {
                fixes = _uow.PositionFixes.GetAll.Where(x => x.Audio == b).ToList();
            }
            else
            {
                Chart chart = _uow.Charts.Get(chartId);
                if (chart == null)
                    return;

                fixes = _uow.PositionFixes.GetAll.Where(x => x.Audio == b && x.Chart == chart).ToList();
            }

            foreach (var fix in fixes)
            {
                fix.Audio = a;
                _uow.PositionFixes.Update(fix);
            }

            if (chartId == 0)
            {
                var ltas = _uow.LabelToAudios.GetAll.Where(x => x.Audio == b).ToList();

                foreach (var lta in ltas)
                {
                    _uow.LabelToAudios.Remove(lta.Id);
                }

                _uow.Audios.Remove(two);
            }

            _uow.Save();
        }

        public void AttachLabels(int audioId, string[] labelNames)
        {
            Audio audio = _uow.Audios.Get(audioId);
            if (audio == null)
                return;

            foreach (var labelName in labelNames)
            {
                string name = labelName.Trim();

                Label label = _uow.Labels.GetAll.FirstOrDefault(x => x.Name == name);

                if (label is null)
                {
                    label = new Label { Name = name };
                    _uow.Labels.Add(label);
                }

                LabelToAudio lta = new LabelToAudio
                {
                    Label = label,
                    Audio = audio
                };

                _uow.LabelToAudios.Add(lta);
            }

            _uow.Save();
        }

        public void RemoveLabels(int audioId)
        {

        }

        public void MergeAll()
        {
            var groups = _uow.Audios.GetAll
                .AsEnumerable()
                .GroupBy(x => new { x.ArtistNormalized, x.TitleNormalized })
                .Where(x => x.Count() > 1)
                .ToList();

            foreach (var group in groups)
            {
                //System.Diagnostics.Debug.WriteLine("group: " + group.Key.ArtistNormalized + " - " + group.Key.TitleNormalized);

                Audio one = group
                    .OrderBy(x => x.Id)
                    .FirstOrDefault();

                foreach (var audio in group)
                {
                    //System.Diagnostics.Debug.WriteLine("--- track: " + audio.Artist + " - " + audio.Title);
                    if (audio.Id != one.Id)
                    {
                        ChangeAudio(one.Id, audio.Id, 0);
                    }
                }
            }

            _uow.Save();
        }

        public void RenameArtist(int audioId, string name)
        {
            Audio audio = _uow.Audios.Get(audioId);
            if (audio == null)
                return;

            string artistNormalized = Normalizer.ArtistNormalize(name);

            audio.Artist = name;
            audio.ArtistNormalized = artistNormalized;

            _uow.Audios.Update(audio);

            _uow.Save();
        }

        public Response SetCover(int id)
        {
            //Audio audio = _uow.Audios.Get(id);
            //if (audio == null)
            //    return new ErrorResponse("wrong id");

            //var path = Directory.GetCurrentDirectory() + "/Uploads/";

            //AppleMusic am = new AppleMusic();
            //am.Auth(_config.Auth.Apple.Key);

            //string url = LabelWorker.GetCoverUrl(am, audio.ArtistNormalized, audio.TitleNormalized);
            //if (url == null)
            //    return new ErrorResponse("cannot get url");

            //audio.ThumbUrl = url;

            //string cover = ImgSaver.SaveFromUrl(path, url, id.ToString());

            //audio.SavedThumb = cover;

            //_uow.Save();

            return new DoneResponse("Done");
        }

        public Response WithoutCover()
        {
            int count = (from a in _uow.Audios.GetAll
                          where a.SavedThumb == "/Uploads/no-image.png"
                          select a).Count();

            return new DoneResponse("without cover: " + count + " audios");
        }

        public Response SetCoverToAll()
        {
            //var audios = (from a in _uow.Audios.GetAll
            //             where a.SavedThumb == "/Uploads/no-image.png"
            //             select a).ToList();

            //var path = Directory.GetCurrentDirectory() + "/Uploads/";

            //AppleMusic am = new AppleMusic();
            //am.Auth(_config.Auth.Apple.Key);

            //int i = 0;
            //foreach (var audio in audios)
            //{
            //    string url = LabelWorker.GetCoverUrl(am, audio.ArtistNormalized, audio.TitleNormalized);
            //    if (url == null)
            //        continue;

            //    audio.ThumbUrl = url;

            //    string cover = ImgSaver.SaveFromUrl(path, url, i.ToString());

            //    audio.SavedThumb = cover;

            //    ++i;
            //    if (i % 200 == 0) _uow.Save();
            //}

            //_uow.Save();

            return new DoneResponse("Done");
        }

        private List<PlaylistDto> GetPlaylistsWithThisTrack(Platform platform, Audio audio)
        {
            var playlists = _uow.Playlists.GetAll.Where(x => x.Platform == platform);

            var playlistsWithThisTrack = new List<PlaylistDto>();

            foreach (var playlist in playlists)
            {
                var tracks = JsonConvert.DeserializeObject<List<PlaylistAudioDto>>(playlist.Tracks);

                var track = tracks?.FirstOrDefault(x =>
                    x.TitleNormalized == audio.TitleNormalized
                    && Normalizer.ArtistEqual(audio.ArtistNormalized, x.ArtistNormalized));

                if (track != null)
                {
                    playlistsWithThisTrack.Add(new PlaylistDto
                    {
                        Name = playlist.Name,
                        Cover = playlist.Cover,
                        Link = playlist.Link,
                        Position = track.Position
                    });
                }
            }

            return playlistsWithThisTrack;
        }
    }
}
