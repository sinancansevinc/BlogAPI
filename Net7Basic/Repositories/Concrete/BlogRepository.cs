using Microsoft.EntityFrameworkCore;
using Net7Basic.Data;
using Net7Basic.Models;
using Net7Basic.Repositories.Abstract;

namespace Net7Basic.Repositories.Concrete
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        //private readonly ApplicationDbContext _context;
        //public BlogRepository(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public IQueryable<Blog> GetBlogs()
        //{
        //    return _context.Blogs.Include(p=>p.Posts);
        //}
        public BlogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
