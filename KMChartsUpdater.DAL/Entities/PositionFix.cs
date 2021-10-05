using System;
using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class PositionFix : Entity
    {
        public virtual Chart Chart { get; set; }

        public virtual ChartFix ChartFix { get; set; }

        public virtual Audio Audio { get; set; }

        public virtual Album Album { get; set; }

        public long Streams { get; set; }

        public long Likes { get; set; }

        public int Position { get; set; }

        public bool IsNew { get; set; }

        public int Shift { get; set; }

        public DateTime Date { get; set; }

        public string Playlists { get; set; }
    }
}
