using System.Collections.Generic;

namespace KMChartsUpdater.BLL.Charts.Models
{
    public class UnifiedPlaylistModel
    {
        public string CoverUrl { get; set; }

        public List<UnifiedAudioModel> Audios { get; set; }
    }
}
