using Net7Basic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net7Basic.Dtos
{
    public class BlogCreateDto
    {
        public string Title { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
