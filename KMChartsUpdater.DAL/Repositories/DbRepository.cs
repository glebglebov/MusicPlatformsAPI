using KMChartsUpdater.DAL.Context;
using KMChartsUpdater.DAL.Entities;
using KMChartsUpdater.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace KMChartsUpdater.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly ApplicationContext _db;
        private readonly DbSet<T> _set;

        public bool AutoSave { get; set; } = false;

        public DbRepository(ApplicationContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public IQueryable<T> GetAll => _set;

        public T Get(int id) => GetAll.SingleOrDefault(item => item.Id == id);

        public void Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException();

            _db.Entry(item).State = EntityState.Added;

            if (AutoSave) Save();
        }

        public void Update(T item)
        {
            if (item is null)
                throw new ArgumentNullException();

            _db.Entry(item).State = EntityState.Modified;

            if (AutoSave) Save();
        }

        public void Remove(int id)
        {
            T item = Get(id);

            if (item is null)
                return;

            _db.Entry(item).State = EntityState.Deleted;

            if (AutoSave) Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
