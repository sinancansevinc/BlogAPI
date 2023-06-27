using Net7Basic.Data;
using Net7Basic.Models;
using Net7Basic.Repositories.Abstract;

namespace Net7Basic.Repositories.Concrete
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
