using System;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;

namespace KMChartsUpdater.BLL.Charts
{
    public abstract class ChartUpdater
    {
        protected readonly UnitOfWork Uow;

        protected ChartUpdater(UnitOfWork uow)
        {
            Uow = uow;
        }

        public abstract void Update(Chart chart);

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
