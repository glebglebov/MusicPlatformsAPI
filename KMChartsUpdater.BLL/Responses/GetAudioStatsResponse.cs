using KMChartsUpdater.BLL.DTO;
using System.Collections.Generic;

namespace KMChartsUpdater.BLL.Responses
{
    public class GetAudioStatsResponse
    {
        public ItemStatsDto Stats { get; set; }

        public int Count { get; set; }

        public ICollection<PositionDto> Items { get; set; }
    }
}
