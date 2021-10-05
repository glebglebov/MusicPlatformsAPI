
namespace KMChartsUpdater.DAL.Entities
{
    public class LabelToAudio : Entity
    {
        public virtual Label Label { get; set; }

        public virtual Audio Audio { get; set; }
    }
}
