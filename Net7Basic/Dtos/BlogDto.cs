using Net7Basic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net7Basic.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Post> Posts { get; } = new List<Post>();

        public ApplicationUser User { get; set; }

    }
}
