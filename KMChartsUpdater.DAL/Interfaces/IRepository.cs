using KMChartsUpdater.DAL.Entities;
using System.Linq;

namespace KMChartsUpdater.DAL.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        bool AutoSave { get; set; }

        IQueryable<T> GetAll { get; }

        T Get(int id);

        void Add(T item);

        void Update(T item);

        void Remove(int id);

        void Save();
    }
}
