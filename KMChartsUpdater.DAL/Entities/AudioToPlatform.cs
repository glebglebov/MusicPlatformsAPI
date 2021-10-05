
namespace KMChartsUpdater.DAL.Entities
{
    public class AudioToPlatform
    {
        public virtual Audio Audio { get; set; }

        public virtual Platform Platform { get; set; }

        public string ContentId { get; set; }

        public string Url { get; set; }
    }
}
