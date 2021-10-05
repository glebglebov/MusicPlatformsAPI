using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public abstract class ChartItem : Entity
    {
        public string Artist { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ThumbUrl { get; set; }

        public int Year { get; set; }

        public bool IsExplicit { get; set; }

        public string ArtistNormalized { get; set; }

        public string TitleNormalized { get; set; }

        public string SavedThumb { get; set; }

        public virtual ICollection<PositionFix> PositionFixes { get; set; }
    }
}
