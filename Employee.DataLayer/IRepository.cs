using System.Linq;

namespace Employee.DataLayer
{
    public interface IRepository<TEntity>
    {
        void Create(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity GetById(params object[] keyValues);

        IQueryable<TEntity> GetEntities();

        void SaveChanges();
    }
}
