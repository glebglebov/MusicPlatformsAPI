using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMChartsUpdater.BLL.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly UnitOfWork _uow;

        public AlbumService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public GetItemInfoResponse<AlbumInfoDto> GetInfo(int id)
        {
            Album album = _uow.Albums.Get(id);
            if (album is null)
                return null;

            AlbumInfoDto info = new AlbumInfoDto
            {
                Year = album.Year,
                Genre = album.Genres
            };

            var response = new GetItemInfoResponse<AlbumInfoDto>
            {
                ItemInfo = info
            };

            return response;
        }

        public async Task<GetAudioStatsResponse> GetAlbumPositions(int id, int chartId)
        {
            var result = new List<PositionDto>();

            Chart chart = _uow.Charts.Get(chartId);
            Album album = _uow.Albums.Get(id);

            if (album is null || chart is null)
                return null;

            var query = from p in _uow.PositionFixes.GetAll
                        where p.Album == album && p.Chart == chart
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

            var stats = new ItemStatsDto
            {
                Chart = chart.Name,
                Best = best,
                Days = days,
                First = firstFix.ToString("dd.MM.yyyy"),
                FirstPlace = first.pos
            };

            var response = new GetAudioStatsResponse
            {
                Stats = stats,
                Count = chart.Count,
                Items = result
            };

            return response;
        }
    }
}
