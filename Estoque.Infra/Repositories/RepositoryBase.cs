using Estoque.Domain.Contracts.Repositories;
using Estoque.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly MicroServiceContext _context;

        public RepositoryBase(MicroServiceContext context)
        {
            _context = context;
        }

        public async Task Add(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            _context.SaveChanges();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public void RemoveAll(TEntity[] objs)
        {
            _context.Set<TEntity>().RemoveRange(objs);
            _context.SaveChanges();
        }

        public void Dispose()
        {

        }
    }
}
