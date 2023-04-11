using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WalletAppBackend.Entities;
using WalletAppBackend.Models;
using WalletAppBackend.Persistence;

namespace WalletAppBackend.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMainDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(IMainDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = _dbSet.Where(expression);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public Task<List<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            return query.ToListAsync(cancellationToken);
        }

        public Task<List<T>> FindAllAsync(CancellationToken cancellationToken)
        {
            return _dbSet.ToListAsync(cancellationToken);
        }
        public PagedResponse<T> GetPaged(IQueryable<T> query, int page, int pageSize)
        {
            int rowCount = query.Count();

            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            int pageCount = query.Count();

            var entities = query.ToList();

            return new PagedResponse<T>() { CurrentPage = page, PageSize = pageSize, RowCount = rowCount, PageCount = pageCount, Results = entities };
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
