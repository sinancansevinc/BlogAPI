using Net7Basic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net7Basic.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Blog Blog { get; set; }
        public ApplicationUser User { get; set; }
    }
}
