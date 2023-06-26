namespace Net7Basic.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Post> Posts { get; } = new List<Post>();
        public string UserId { get; set; }
        public ApplicationUser User = null;

    }
}
