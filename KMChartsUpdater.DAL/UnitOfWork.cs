using KMChartsUpdater.DAL.Context;
using KMChartsUpdater.DAL.Entities;
using KMChartsUpdater.DAL.Interfaces;
using KMChartsUpdater.DAL.Repositories;
using System;

namespace KMChartsUpdater.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationContext _db;

        private IRepository<AccessToken> _accessTokens;
        private IRepository<Platform> _platforms;
        private IRepository<Chart> _charts;
        private IRepository<ChartFix> _chartFixes;
        private IRepository<Audio> _audios;
        private IRepository<Album> _albums;
        private IRepository<PositionFix> _positionFixes;
        private IRepository<Label> _labels;
        private IRepository<LabelToAudio> _labelToAudios;
        private IRepository<Playlist> _playlists;
        private IRepository<Account> _accounts;
        private IRepository<AudioTask> _audioTasks;
        private IRepository<Report> _reports;

        public UnitOfWork(ApplicationContext db)
        {
            _db = db;
        }

        public IRepository<AccessToken> AccessTokens
        {
            get
            {
                if (_accessTokens == null) _accessTokens = new DbRepository<AccessToken>(_db);
                return _accessTokens;
            }
        }

        public IRepository<Platform> Platforms
        {
            get
            {
                if (_platforms == null) _platforms = new DbRepository<Platform>(_db);
                return _platforms;
            }
        }

        public IRepository<Chart> Charts
        {
            get
            {
                if (_charts == null) _charts = new DbRepository<Chart>(_db);
                return _charts;
            }
        }

        public IRepository<ChartFix> ChartFixes
        {
            get
            {
                if (_chartFixes == null) _chartFixes = new DbRepository<ChartFix>(_db);
                return _chartFixes;
            }
        }

        public IRepository<Audio> Audios
        {
            get
            {
                if (_audios == null) _audios = new DbRepository<Audio>(_db);
                return _audios;
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
                if (_albums == null) _albums = new DbRepository<Album>(_db);
                return _albums;
            }
        }

        public IRepository<PositionFix> PositionFixes
        {
            get
            {
                if (_positionFixes == null) _positionFixes = new DbRepository<PositionFix>(_db);
                return _positionFixes;
            }
        }

        public IRepository<Label> Labels
        {
            get
            {
                if (_labels == null) _labels = new DbRepository<Label>(_db);
                return _labels;
            }
        }

        public IRepository<LabelToAudio> LabelToAudios
        {
            get
            {
                if (_labelToAudios == null) _labelToAudios = new DbRepository<LabelToAudio>(_db);
                return _labelToAudios;
            }
        }

        public IRepository<Playlist> Playlists
        {
            get
            {
                if (_playlists == null) _playlists = new DbRepository<Playlist>(_db);
                return _playlists;
            }
        }

        public IRepository<Account> Accounts
        {
            get
            {
                if (_accounts == null)
                    _accounts = new DbRepository<Account>(_db);
                return _accounts;
            }
        }

        public IRepository<AudioTask> AudioTasks
        {
            get
            {
                if (_audioTasks == null)
                    _audioTasks = new DbRepository<AudioTask>(_db);
                return _audioTasks;
            }
        }

        public IRepository<Report> Reports
        {
            get
            {
                if (_reports == null)
                    _reports = new DbRepository<Report>(_db);
                return _reports;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
