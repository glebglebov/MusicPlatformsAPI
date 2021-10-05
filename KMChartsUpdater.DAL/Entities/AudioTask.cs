using System;
using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class AudioTask : Entity
    {
        public DateTime Created { get; set; }

        public bool IsActive { get; set; }

        public virtual Audio Audio { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
