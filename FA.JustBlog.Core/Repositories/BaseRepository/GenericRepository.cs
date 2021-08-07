using FA.JustBlog.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories.BaseRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly JustBlogContext context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            context = new JustBlogContext();
            _dbSet = context.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _dbSet.Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            context.Entry(entity).State = EntityState.Deleted;

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            context.Entry(entity).State = EntityState.Modified;
            return await context.SaveChangesAsync() > 0;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public int Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            _dbSet.Add(entity);
            return context.SaveChanges();
        }

        public bool Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            context.Entry(entity).State = EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
