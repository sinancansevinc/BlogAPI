using Microsoft.AspNetCore.Identity;

namespace Net7Basic.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Blog> Blogs{ get; } = new List<Blog>();


    }
}
