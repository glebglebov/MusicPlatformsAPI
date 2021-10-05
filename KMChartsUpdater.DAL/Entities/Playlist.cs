namespace KMChartsUpdater.DAL.Entities
{
    public class Playlist : Entity
    {
        public string Name { get; set; }

        public string Cover { get; set; }

        public string Link { get; set; }

        public string IdOnPlatform { get; set; }

        public virtual Platform Platform { get; set; }

        public string Tracks { get; set; } // JSON
    }
}
