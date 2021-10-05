using System.IO;
using System.Linq;
using KMChartsUpdater.BLL.Charts.Models;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;

namespace KMChartsUpdater.BLL.Charts
{
    public class AlbumChartUpdater : ChartUpdater
    {
        public AlbumChartUpdater(UnitOfWork uow) : base(uow)
        {

        }

        public override void Update(Chart chart)
        {
            var api = ApiFactory.GetByPlatformCode(chart.Platform.Code);

            if (api == null || chart.Type != "album")
                return;

            api.Auth();

            var items = api.GetAlbumChart(chart.Code);

            ChartFix lastChartFix = chart.ChartFixes
                .OrderBy(x => x.Updated)
                .LastOrDefault();

            var chartFix = CreateChartFix(chart);

            foreach (var item in items)
            {
                Album album = GetOrCreateAlbum(item);

                var fix = UpdatePosition(album, lastChartFix, item.Position);

                fix.ChartFix = chartFix;
                fix.Streams = item.Plays;
                fix.Chart = chart;
                fix.Date = chartFix.Updated;

                Uow.PositionFixes.Add(fix);
            }

            Uow.Save();
        }

        private Album GetOrCreateAlbum(UnifiedAlbumModel item)
        {
            Album album = Uow.Albums.GetAll
                .FirstOrDefault(x => x.Artist == item.Artist && x.Title == item.Title);

            if (album is null)
            {
                string thumb = ImgSaver.SaveFromUrl("/Uploads/", item.ThumbUrl);

                string genre = string.Join(", ", item.Genres);

                album = new Album
                {
                    Artist = item.Artist,
                    Title = item.Title,
                    Year = item.Year,
                    IsExplicit = item.IsExplicit,
                    ThumbUrl = item.ThumbUrl,
                    SavedThumb = thumb,
                    Genres = genre
                };

                Uow.Albums.Add(album);
                Uow.Save();
            }

            return album;
        }

        private PositionFix UpdatePosition(Album item, ChartFix lastChartFix, int position)
        {
            PositionFix prevFix = lastChartFix?.PositionFixes
                .FirstOrDefault(x => x.Album == item);

            bool isNew = true;
            int shift = 0;

            if (prevFix != null)
            {
                isNew = false;
                shift = prevFix.Position - position;
            }

            var positionFix = new PositionFix
            {
                Album = item,
                Position = position,
                IsNew = isNew,
                Shift = shift
            };

            return positionFix;
        }
    }
}
