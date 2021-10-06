using System.Collections.Generic;

namespace KMChartsUpdater.BLL.ReportGenerator
{
    public class ReportGroup
    {
        public string Name { get; set; }

        public List<ReportElement> Elements { get; set; }
    }
}
