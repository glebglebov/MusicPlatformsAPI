using KMChartsUpdater.BLL.DTO;
using System.Collections.Generic;

namespace KMChartsUpdater.BLL.Responses
{
    public class GetChartFixResponse : Response
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public int Id { get; set; }

        public ICollection<ChartItemDto> Items { get; set; }
    }
}
