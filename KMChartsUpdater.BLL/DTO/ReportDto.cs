using System;

namespace KMChartsUpdater.BLL.DTO
{
    public class ReportDto
    {
        public string Name { get; set; }

        public string Filename { get; set; }

        public string FilePath { get; set; }

        public DateTime Updated { get; set; }
    }
}
