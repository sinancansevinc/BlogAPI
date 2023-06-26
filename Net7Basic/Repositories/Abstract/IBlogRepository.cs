using Net7Basic.Models;

namespace Net7Basic.Repositories.Abstract
{
    public interface IBlogRepository:IGenericRepository<Blog>
    {
        //IQueryable<Blog> GetBlogs();
    }
}