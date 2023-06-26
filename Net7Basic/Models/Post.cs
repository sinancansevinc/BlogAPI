using System.ComponentModel.DataAnnotations.Schema;

namespace Net7Basic.Models
{
    public class Post
    {
        public int Id { get; set; }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
