using System.Collections.Generic;

namespace KMChartsUpdater.DAL.Entities
{
    public class Platform : Entity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public virtual ICollection<Chart> Charts { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
