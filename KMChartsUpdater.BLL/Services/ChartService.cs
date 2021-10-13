using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL;
using System.Net;
using KMChartsUpdater.BLL.Charts;

namespace KMChartsUpdater.BLL.Services
{
    public class ChartService : IChartService
    {
        private readonly UnitOfWork _uow;

        private readonly IApiAdapterFactory _apiAdapterFactory;

        public ChartService(UnitOfWork uow, IApiAdapterFactory apiAdapterFactory)
        {
            _uow = uow;
            _apiAdapterFactory = apiAdapterFactory;
        }

        public GetChartFixResponse GetChart(int id, int offset, int limit, string date = null)
        {
            var chart = _uow.Charts.Get(id);

            ChartFix lastChartFix = (date == null)
                ? chart?.ChartFixes.OrderBy(x => x.Updated).LastOrDefault()
                : chart?.ChartFixes.LastOrDefault(item => item.NormalDate == date);

            if (lastChartFix == null)
                return null;

            List<ChartItemDto> audios = new List<ChartItemDto>();

            var fixes = lastChartFix.PositionFixes
                .OrderBy(x => x.Position)
                .Skip(offset)
                .Take(limit);

            foreach (var fix in fixes)
            {
                ChartInfoDto chartInfo = new ChartInfoDto
                {
                    Position = fix.Position,
                    IsNew = fix.IsNew,
                    Shift = fix.Shift,
                    Likes = fix.Likes,
                    Streams = fix.Streams
                };

                var dbLabels = _uow.LabelToAudios.GetAll
                    .Where(x => x.Audio == fix.Audio)
                    .Select(x => x.Label)
                    .ToList();

                List<LabelDto> labels = new List<LabelDto>();

                foreach (var label in dbLabels)
                {
                    LabelDto labelDto = new LabelDto
                    {
                        Id = label.Id,
                        Name = label.Name
                    };

                    labels.Add(labelDto);
                }

                ChartItem item;

                if (fix.Audio == null)
                {
                    item = fix.Album;
                }
                else
                {
                    item = fix.Audio;
                }

                ChartItemDto audio = new ChartItemDto
                {
                    Id = item.Id,
                    Type = chart.Type,
                    Artist = WebUtility.HtmlEncode(item.Artist),
                    Title = WebUtility.HtmlEncode(item.Title),
                    Subtitle = WebUtility.HtmlEncode(item.Subtitle),
                    ThumbUrl = item.SavedThumb,
                    IsExplicit = item.IsExplicit,
                    Labels = labels,
                    ChartInfo = chartInfo
                };

                if (item is Album album)
                {
                    audio.Year = album.Year;
                    audio.Genre = album.Genres;
                }

                audios.Add(audio);
            }

            var response = new GetChartFixResponse
            {
                Title = chart.Name,
                Type = chart.Type,
                Id = lastChartFix.Id,
                Items = audios
            };

            return response;
        }

        public void UpdateChart(int chartId)
        {
            Chart chart = _uow.Charts.Get(chartId);

            if (chart == null)
                return;

            ChartUpdater updater;

            if (chart.Type == "audio")
            {
                updater = new AudioChartUpdater(_uow, _apiAdapterFactory);
            }
            else if (chart.Type == "album")
            {
                updater = new AlbumChartUpdater(_uow, _apiAdapterFactory);
            }
            else
            {
                return;
            }

            updater.Update(chart);
        }

        public ChartFixDto GetAllFixes(int id)
        {
            Chart chart = _uow.Charts.Get(id);
            if (chart == null)
                return null;

            var fixes = chart.ChartFixes.OrderBy(x => x.Updated);

            DateTime first = fixes.FirstOrDefault().Updated;
            DateTime last = fixes.LastOrDefault().Updated;

            return new ChartFixDto { FirstFix = first, LastFix = last };
        }

        public void DeleteFix(int id)
        {
            ChartFix fix = _uow.ChartFixes.Get(id);
            if (fix == null)
                return;

            foreach (var pos in fix.PositionFixes.ToArray())
            {
                _uow.PositionFixes.Remove(pos.Id);
            }

            _uow.ChartFixes.Remove(id);

            _uow.Save();
        }

        public void DeleteChart(int id)
        {
            var audios = _uow.Audios.GetAll
                .Where(x => x.TitleNormalized == null)
                .ToArray();

            foreach (var audio in audios)
            {
                var ltos = _uow.LabelToAudios.GetAll
                    .Where(x => x.Audio == audio)
                    .ToArray();

                foreach (var lto in ltos)
                {
                    _uow.LabelToAudios.Remove(lto.Id);
                }

                _uow.Audios.Remove(audio.Id);

                _uow.Save();
            }
        }

        public void Date(int id, int day, int month, int year)
        {
            var chartFix = _uow.ChartFixes.Get(id);
            if (chartFix is null)
                return;

            DateTime date = new DateTime(year, month, day);
            string dateString = date.ToString("dd.MM.yyyy");

            chartFix.Updated = date;
            chartFix.NormalDate = dateString;

            _uow.ChartFixes.Update(chartFix);

            foreach (var fix in chartFix.PositionFixes.ToList())
            {
                fix.Date = date;
                _uow.PositionFixes.Update(fix);
            }

            _uow.Save();
        }
    }
}
