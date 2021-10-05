using System;

namespace KMChartsUpdater.DAL.Entities
{
    public class Report : Entity
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string Playlists { get; set; } // JSON

        public DateTime Updated { get; set; }

        public virtual AudioTask AudioTask { get; set; }
    }
}
