using System.Collections.Generic;

namespace KMChartsUpdater.BLL.DTO
{
    public class ItemStatsDto
    {
        public string Chart { get; set; }

        public int ChartId { get; set; }

        public int Best { get; set; }

        public int Days { get; set; }

        public string First { get; set; }

        public int FirstPlace { get; set; }

        public List<PlaylistDto> Playlists { get; set; }
    }
}
