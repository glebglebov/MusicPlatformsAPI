using KMChartsUpdater.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KMChartsUpdater.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AccessToken> AccessTokens { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<Chart> Charts { get; set; }

        public DbSet<ChartFix> ChartFixes { get; set; }

        public DbSet<Audio> Audios { get; set; }

        public DbSet<PositionFix> PositionFixes { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<LabelToAudio> LabelToAudios { get; set; }

        public DbSet<Playlist> Playlists { get; set; }


        public DbSet<Account> Accounts { get; set; }

        public DbSet<AudioTask> AudioTasks { get; set; }

        public DbSet<Report> Reports { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
