using System.Linq.Expressions;
using WalletAppBackend.Entities;
using WalletAppBackend.Models;

namespace WalletAppBackend.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T?> GetById(int id);

        IQueryable<T> GetQueryable();

        IQueryable<T> Find(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<List<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default);

        PagedResponse<T> GetPaged(IQueryable<T> query, int page, int pageSize);

        Task<List<T>> FindAllAsync(CancellationToken cancellationToken);

        T Add(T entity);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
