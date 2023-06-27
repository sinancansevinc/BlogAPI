using Microsoft.EntityFrameworkCore;
using Net7Basic.Data;
using Net7Basic.Models;
using Net7Basic.Repositories.Abstract;

namespace Net7Basic.Repositories.Concrete
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {

        public BlogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
