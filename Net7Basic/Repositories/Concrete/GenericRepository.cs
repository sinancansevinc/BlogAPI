using Microsoft.EntityFrameworkCore;
using Net7Basic.Data;
using System.Linq.Expressions;
using System.Linq;
using Net7Basic.Repositories.Abstract;

namespace Net7Basic.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            if (pageSize > 0)
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }


            return await query.ToListAsync();
        }

        public async Task Create(T T)
        {
            await _dbSet.AddAsync(T);
            await Save();
        }
        public async Task Remove(T T)
        {
            _dbSet.Remove(T);
            await Save();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
