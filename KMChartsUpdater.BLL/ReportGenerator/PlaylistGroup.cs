using System.Collections.Generic;

namespace KMChartsUpdater.BLL.ReportGenerator
{
    public class PlaylistGroup
    {
        public string Name { get; set; }

        public List<ReportElement> Elements { get; set; }
    }
}
