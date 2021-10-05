using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class Chart : Entity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Count { get; set; }

        public string Code { get; set; }

        public virtual Platform Platform { get; set; }

        public virtual ICollection<ChartFix> ChartFixes { get; set; }

        public virtual ICollection<PositionFix> PositionFixes { get; set; }
    }
}
