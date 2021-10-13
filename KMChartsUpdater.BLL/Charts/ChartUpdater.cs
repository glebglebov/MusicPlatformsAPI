using System;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;

namespace KMChartsUpdater.BLL.Charts
{
    public abstract class ChartUpdater
    {
        protected readonly UnitOfWork Uow;

        protected readonly IApiAdapterFactory ApiFactory;

        protected ChartUpdater(UnitOfWork uow, IApiAdapterFactory apiFactory)
        {
            Uow = uow;
            ApiFactory = apiFactory;
        }

        public abstract void Update(Chart chart);

        protected IMusicApiAdapter GetApiInstance(Chart chart)
        {
            IMusicApiAdapter api = (chart?.Platform.Id) switch
            {
                1 => ApiFactory.CreateVkApiAdapter(false),
                2 => ApiFactory.CreateSpotifyApiAdapter(false),
                3 => ApiFactory.CreateYandexMusicApiAdapter(false),
                4 => ApiFactory.CreateAppleMusicApiAdapter(false),
                _ => null,
            };

            return api;
        }

        protected ChartFix CreateChartFix(Chart chart)
        {
            var fix = new ChartFix
            {
                Chart = chart,
                Updated = DateTime.Now,
                NormalDate = DateTime.Now.ToString("dd.MM.yyyy")
            };

            Uow.ChartFixes.Add(fix);
            Uow.Save();

            return fix;
        }
    }
}
