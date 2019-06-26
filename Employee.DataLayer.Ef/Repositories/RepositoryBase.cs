using AutoMapper;
using Employee.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Employee.DataLayerEf.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly EmployeeContext context;

        private readonly IMapper mapper;

        public RepositoryBase(EmployeeContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void Create(TEntity entity) => context.Set<TEntity>().Add(entity);

        public TEntity GetById(params object[] keyValues) => context.Set<TEntity>().Find(keyValues);

        public IQueryable<TEntity> GetEntities() => context.Set<TEntity>().AsNoTracking();

        public TEntity Update(TEntity entity)
        {
            var target = GetEntity(entity);

            if (target == null)
            {
                throw new KeyNotFoundException();
            }

            mapper.Map(entity, target);

            return target;
        }

        public void SaveChanges() => context.SaveChanges(); 

        protected abstract TEntity GetEntity(TEntity entity);
    }
}
