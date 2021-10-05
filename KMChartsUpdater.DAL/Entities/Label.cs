using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class Label : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<LabelToAudio> LabelToAudios { get; set; }
    }
}
