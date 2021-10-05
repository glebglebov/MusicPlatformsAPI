using System.Collections.Generic;

namespace KMChartsUpdater.BLL.DTO
{
    public class PlaylistWithTracksDto
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public string Cover { get; set; }

        public List<PlaylistAudioDto> Tracks { get; set; }
    }
}
