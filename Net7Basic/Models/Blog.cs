namespace Net7Basic.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public ICollection<Post> Posts { get; } = new List<Post>();
    }
}
