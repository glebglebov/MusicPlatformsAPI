using System;
using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class ChartFix : Entity
    {
        public virtual Chart Chart { get; set; }

        public DateTime Updated { get; set; }

        public string NormalDate { get; set; }

        public virtual ICollection<PositionFix> PositionFixes { get; set; }
    }
}
