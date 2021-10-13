using System.Collections.Generic;

namespace KMChartsUpdater.BLL.ReportGenerator
{
    public class ReportContent
    {
        public string ReportTitle { get; set; }

        public string ReportSubtitle { get; set; }

        public List<PlaylistGroup> Groups { get; set; }
    }
}
