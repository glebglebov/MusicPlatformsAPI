using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class Album : ChartItem
    {
        public string Genres { get; set; }

        public string ContentId { get; set; }
    }
}
