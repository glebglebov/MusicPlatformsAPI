using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class Audio : ChartItem
    {
        public string TrackCode { get; set; }

        public virtual ICollection<LabelToAudio> LabelToAudios { get; set; }

        public virtual ICollection<AudioTask> Reports { get; set; }
    }
}
