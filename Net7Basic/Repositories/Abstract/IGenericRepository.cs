using System.Linq.Expressions;

namespace Net7Basic.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task Create(T entity);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1);
        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
        Task Remove(T entity);
        Task Save();
    }
}